# Definition
Token is a `x-token` header sent by the client to the server every time you submit ascore.

# Format
A string consisting of [0-N characters][82 bytes of token data];
Token data is
```
[0      32           40                       80  82]
[       ==============                              ] -  ClientTimeHex
[=====================                              ] -  ClientData
[========                                           ] -  ClientHash
[                    ===========================    ] -  Expected
[                                             ======] -  Version
```

Notes:
ClientData is a set of ClientTimeHex + ClientHash

Based on this data, the "expected" value is calculated as
```php
$build = Build::firstWhere([
    'hash' => $input['clientHash'],
    'allow_ranking' => true,
]);

$computed = hash_hmac(
    'sha1',
    $input['clientData'],
    static::getKey($build),
    true,
);
```

After that, the check is done.

Notes:
* ClientTime check is `if (abs($now - $input['clientTime']) > $GLOBALS['cfg']['osu']['client']['token_lifetime']) {`
  So, better set it to nearby the end of the universe.
(LIKELY) Client version is not used

Data needed to complete the test:
* ClientHash
* BuildID

Notes:
(LIKELY) ClientHash is *NOT* the `File.OpenRead(typeof(OsuGameBase).Assembly.Location))`!

clienthash: 613db83fe458912a286a5ca3adca0f9f
buildid: b702d10a30e44886209861479673985f
version: "12"
expected:


# Source

https://github.com/ppy/osu-web/blob/3103311c07f05a4f0287e7500dccf11e17aad3c8/app/Http/Controllers/ScoreTokensController.php#L50

```php
$buildId = ClientCheck::parseToken($request)['buildId'];
```

https://github.com/ppy/osu-web/blob/master/app/Libraries/ClientCheck.php#L16

```php
if ($token === null) {
    throw new ClientCheckParseTokenException('missing token header');
}

$input = static::splitToken($token);

$build = Build::firstWhere([
    'hash' => $input['clientHash'],
    'allow_ranking' => true,
]);

if ($build === null) {
    throw new ClientCheckParseTokenException('invalid client hash');
}

$ret['buildId'] = $build->getKey();

$computed = hash_hmac(
    'sha1',
    $input['clientData'],
    static::getKey($build),
    true,
);
```

https://github.com/ppy/osu-web/blob/master/app/Libraries/ClientCheck.php#L91

```php
private static function splitToken(string $token): array
{
    $data = substr($token, -82);
    $clientTimeHex = substr($data, 32, 8);
    $clientTime = strlen($clientTimeHex) === 8
        ? unpack('V', hex2bin($clientTimeHex))[1]
        : 0;

    return [
        'clientData' => substr($data, 0, 40),
        'clientHash' => hex2bin(substr($data, 0, 32)),
        'clientTime' => $clientTime,
        'expected' => hex2bin(substr($data, 40, 40)),
        'version' => substr($data, 80, 2),
    ];
}
```

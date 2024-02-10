# OSU(lazer) Anti Anti Cheat Project

Recently, ppy emerged a spyware called "Osu anticheat" into osu lazer.

## Warning for osu lazer player

DO NOT RUN any executable provided by ppy/osu repository as `release`.
These binaries contain spyware and may potentialy be malware.

**PEPPY is known for screeshoting player desktop..**
[a similar reddit post, not a proof](https://www.reddit.com/r/osugame/comments/4ky41w/osu_source_code_leaked_confirms_ppy_and_devs_are/)

If anyone has a dump of the other thread with proofs, please pull request.

## Warning for cheaters/h0xx1rs

Please don't cheat and don't use this code to cheat =].

## Therefore

A OSU-AAC is a project that aims to completely disable osu-lazer client-side anticheat and let people play osu lazer online with self-hosted binaries.

If you want to participate, please look at the `token.md` and `symbols.md` file;

## Anticheat structure

Anticheat is located in `Osu.Game.Auth.dll`, that can be found nearby the `osu!` binary.
It loads as a dynamic library and links a `sign` into `ApiAccess` and `HubClientConnector`.

Please note, that `Osu.Game` is also infected. Offical osu repo does not contain `sign` delegate, while binary release code does.

Peppy obviously has a patch that is applied manually every single release.

After doing that, a secret set of tokens are extracted/imported into both server and client anticheat.
The tokens are used for signing **secured** methods like score submitting.
Whats a shame, is that without a signature (`x-token` header) YOU CANNOT play multiplayer.

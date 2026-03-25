# osu! AAC

An anti-anti-cheat for osu! that intercepts network communications to bypass server-side validation.

## Purpose

The official osu! multiplayer system includes anti-cheat mechanisms that can incorrectly flag users. This project provides an alternative approach to interact with the game's network layer.

## Disclaimer

**Do not use this for cheating or gaining an unfair advantage.** This project is for educational and research purposes only. Using it to gain unfair advantages may result in bans from osu! and may violate the game's terms of service.

## Documentation

- [`symbols.md`](./symbols.md) - C# hook implementations for network interception
- [`token.md`](./token.md) - Token format specification and reverse engineering documentation

## How it works

This project uses reflection-based hooks to intercept API requests and modify authentication tokens before they reach osu!'s servers. See the documentation files for implementation details.

## License

Use responsibly and only for your own educational purposes.

# AutoClock


[![GitHub release (latest by date including pre-releases)](https://img.shields.io/github/v/release/ReturnNefe/AutoClock?include_prereleases&style=for-the-badge)](https://github.com/ReturnNefe/AutoClock/releases) ![GitHub all releases](https://img.shields.io/github/downloads/ReturnNefe/AutoClock/total?style=for-the-badge)

Auto Clock-in some particular websites every day. 😋

AutoClock can clocks automatically by loading various plugins, you just need to run it in the server.

_(This is an experimental project for [Nefe.PluginCore](https://github.com/ReturnNefe/PluginCore))_

## Requirements

AutoClock is developed based on .NET 6, so it can run on Windows / macOS / Linux.

you need to install ``NET 6 Runtime``.

## Plugins

The table below lists the supported plugins.

|Name|Author|Description|Link|
|:--:|:--:|:--:|:--:|
|EmailSender|ReturnNefe|Send an email when plugins failed to clock-in.|https://github.com/ReturnNefe/AutoClock.EmailSender/|
|Luogu|ReturnNefe|A clocker for [luogu.com.cn](https://www.luogu.com.cn).|https://github.com/ReturnNefe/AutoClock.Luogu/|
|_Bilibili (Coming)_|ReturnNefe|A clocker for [bilibili.com](https://www.bilibili.com/).||

## Installing

1. Download the program in [Releases](https://github.com/ReturnNefe/AutoClock/releases).
2. Select the plugin you need, download them into ``plugins`` folder. The name of directory must be same as the name of the plugin.
    e.g.
    ✅ ``plugins/PluginA/PluginA.dll``
    ❌ ``plugins/AutoClock.PluginA/PluginA.dll``
3. Configure them by reading the plugins' instructions.
4. Run it (in the server).
    ```shell
    ./AutoClock
    ```
    
## License

[MIT](LICENSE) © ReturnNefe
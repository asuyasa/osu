﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Linq;
using osu.Framework.Screens;
using osu.Game.Online.API;
using osu.Game.Online.Rooms;
using osu.Game.Screens.OnlinePlay.Components;
using osu.Game.Screens.Select;

namespace osu.Game.Screens.OnlinePlay.Playlists
{
    public class PlaylistsSongSelect : OnlinePlaySongSelect
    {
        public PlaylistsSongSelect(Room room)
            : base(room)
        {
        }

        protected override BeatmapDetailArea CreateBeatmapDetailArea() => new MatchBeatmapDetailArea
        {
            CreateNewItem = createNewItem
        };

        protected override void SelectItem(PlaylistItem item)
        {
            switch (Playlist.Count)
            {
                case 0:
                    createNewItem();
                    break;

                case 1:
                    Playlist.Clear();
                    createNewItem();
                    break;
            }

            this.Exit();
        }

        private void createNewItem()
        {
            PlaylistItem item = new PlaylistItem
            {
                ID = Playlist.Count == 0 ? 0 : Playlist.Max(p => p.ID) + 1,
                Beatmap = { Value = Beatmap.Value.BeatmapInfo },
                RulesetID = Ruleset.Value.OnlineID,
                RequiredMods = Mods.Value.Select(m => new APIMod(m)).ToArray(),
                AllowedMods = FreeMods.Value.Select(m => new APIMod(m)).ToArray()
            };

            Playlist.Add(item);
        }
    }
}

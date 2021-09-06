using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.ViewControllers;
using System;
using TMPro;

namespace Themely.UI
{
    [HotReload(RelativePathToLayout = @"OGPauseMenu")]
    [ViewDefinition("Themely.UI.OGPauseMenu.bsml")]
    internal class PauseMenu : BSMLAutomaticViewController
    {
        public event Action didPressMenuButtonEvent;
        public event Action didPressRestartButtonEvent;
        public event Action didPressContinueButtonEvent;

        [UIComponent("songNameLabel")] readonly TextMeshProUGUI _songNameLabel = null;
        [UIComponent("songAuthorNameLabel")] readonly TextMeshProUGUI _songAuthorNameLabel = null;
        [UIComponent("beatmapDifficultyNameLabel")] readonly TextMeshProUGUI _beatmapDifficultyNameLabel = null;

        public void Setup(IPreviewBeatmapLevel previewBeatmapLevel, BeatmapCharacteristicSO beatmapCharacteristic, BeatmapDifficulty beatmapDifficulty)
        {
            _songNameLabel.text += previewBeatmapLevel.songName;
            _songAuthorNameLabel.text += previewBeatmapLevel.songAuthorName;
            _beatmapDifficultyNameLabel.text += beatmapDifficulty.Name();
        }

        [UIAction("menuButton")]
        private void OnMenuButton()
        {
            var action = didPressMenuButtonEvent;
            if (action == null)
                return;

            action();
        }

        [UIAction("restartButton")]
        private void OnRestartButton()
        {
            var action = didPressRestartButtonEvent;
            if (action == null)
                return;

            action();
        }

        [UIAction("continueButton")]
        private void OnContinueButton()
        {
            var action = didPressContinueButtonEvent;
            if (action == null)
                return;

            action();
        }
    }
}

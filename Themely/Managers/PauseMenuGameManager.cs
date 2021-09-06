using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.FloatingScreen;
using HMUI;
using IPA.Utilities;
using System;
using Themely.UI;
using UnityEngine;
using Zenject;

namespace Themely.Managers
{
    public class PauseMenuGameManager : IInitializable, IDisposable
    {
        private const string CanvasPath = "Wrapper/MenuWrapper/Canvas";
        private const string MainBarPath = CanvasPath + "/MainBar";

        private PauseMenuManager _pauseMenuManager;
        private PauseController _pauseController;

        private bool _firstActivation = true;

        public PauseMenuGameManager(PauseMenuManager pauseMenuManager, PauseController pauseController)
        {
            _pauseMenuManager = pauseMenuManager;
            _pauseController = pauseController;
        }

        public void Initialize()
        {
            _pauseController.didPauseEvent += HandleDidPauseEvent;
        }

        public void Dispose()
        {
            _pauseController.didPauseEvent -= HandleDidPauseEvent;
        }

        private GameObject GetCanvasGameObject()
        {
            return _pauseMenuManager.transform.Find(CanvasPath).gameObject;
        }

        private GameObject GetMainBarGameObject()
        {
            return _pauseMenuManager.transform.Find(MainBarPath).gameObject;
        }

        private void SetMainBarActive(bool value)
        {
            GetMainBarGameObject().SetActive(value);
        }

        private void HandleDidPauseEvent()
        {
            if (_firstActivation)
            {
                SetMainBarActive(false); // Hide the original MainBar, we only want our custom pause menu.

                var curvedCanvasSettings = GetCanvasGameObject().GetComponent<CurvedCanvasSettings>();
                curvedCanvasSettings.SetRadius(0f);

                var customMainBar = new GameObject("CustomMainBar", typeof(RectTransform));
                customMainBar.transform.SetParent(GetCanvasGameObject().transform);

                var floatingScreen = FloatingScreen.CreateFloatingScreen(new Vector2(128, 64), false, Vector3.zero, Quaternion.Euler(0, 0, 0));
                var pauseMenu = BeatSaberUI.CreateViewController<PauseMenu>();
                floatingScreen.SetRootViewController(pauseMenu, ViewController.AnimationType.None);
                floatingScreen.gameObject.transform.SetParent(customMainBar.transform);

                // Setup our PauseMenu view controller.
                pauseMenu.didPressMenuButtonEvent += () => _pauseMenuManager.MenuButtonPressed();
                pauseMenu.didPressRestartButtonEvent += () => _pauseMenuManager.RestartButtonPressed();
                pauseMenu.didPressContinueButtonEvent += () => _pauseMenuManager.ContinueButtonPressed();
                var initData = _pauseMenuManager.GetField<PauseMenuManager.InitData, PauseMenuManager>("_initData");
                pauseMenu.Setup(initData.previewBeatmapLevel, initData.beatmapCharacteristic, initData.beatmapDifficulty);

                customMainBar.transform.position = new Vector3(0, 1, 2); // Hack to fix BSML adjustment (sigh...)
            }

            _firstActivation = false;
        }
    }
}

using Sirenix.OdinInspector;
using UnityEngine;

namespace Gyvr.Mythril2D
{
    public enum EAudioChannel
    {
        [LabelText("背景音乐")] BackgroundMusic,
        [LabelText("背景音效")] BackgroundSound,
        [LabelText("界面音效")] InterfaceSoundFX,
        [LabelText("游戏音效")] GameplaySoundFX,
        [LabelText("其他")] Miscellaneous
    }


    public class AudioSystem : AGameSystem
    {
        [SerializeField] private SerializableDictionary<EAudioChannel, AudioChannel> m_audioChannels;

        public override void OnSystemStart()
        {
            GameManager.NotificationSystem.audioPlaybackRequested.AddListener(DispatchAudioPlaybackRequest);
        }

        public override void OnSystemStop()
        {
            GameManager.NotificationSystem.audioPlaybackRequested.RemoveListener(DispatchAudioPlaybackRequest);
        }

        private void DispatchAudioPlaybackRequest(AudioClipResolver audioClipResolver)
        {
            if (audioClipResolver &&
                m_audioChannels.TryGetValue(audioClipResolver.targetChannel, out AudioChannel channel))
            {
                channel.Play(audioClipResolver);
            }
        }

        public AudioClipResolver GetLastPlayedAudioClipResolver(EAudioChannel channel)
        {
            if (m_audioChannels.TryGetValue(channel, out AudioChannel channelInstance))
            {
                return channelInstance.lastPlayedAudioClipResolver;
            }

            return null;
        }
    }
}
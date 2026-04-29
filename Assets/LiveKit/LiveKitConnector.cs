using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine.Networking;
using LiveKit;
using LiveKit.Proto;
using RoomOptions = LiveKit.RoomOptions;
using Newtonsoft.Json;
using System.Threading.Tasks;
using UnityEngine.Android;
[System.Serializable] public class EmotionMessage
{
    public string emotion;
}

public class LiveKitConnector : MonoBehaviour
{
    public Room room;

    [SerializeField] private GameObject wilhelm_body;
    [SerializeField] private GameObject wilhelm_tell;
    Dictionary<string, GameObject> _audioObjects = new();
    List<RtcAudioSource> _rtcAudioSources = new();
    Animator anim;
    private MicrophoneSource rtcSource;
    public class TokenRequest
    {
        public string room_name;
        public string participant_identity;
        public string participant_name;
    }

    [System.Serializable]
    public class TokenResponse
    {
        public string server_url;
        public string participant_token;
    }
    IEnumerator Start()
    {
        room = new Room();
        room.TrackSubscribed += TrackSubscribed;
        var requestData = new TokenRequest
        {
            room_name = "wilhelms_room" + System.Guid.NewGuid().ToString("N"),
            participant_identity = "testperson_" + System.Guid.NewGuid().ToString("N"),
            participant_name = "testperson",

        };
        room.ParticipantAttributesChanged += onParticipantAttributesChanged;

        string json = JsonConvert.SerializeObject(requestData);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
        using(UnityWebRequest req = new UnityWebRequest("https://tokenserver-holy-sun-9307.fly.dev/getToken", "POST"))
        {
            req.uploadHandler = new UploadHandlerRaw(bodyRaw);
            req.downloadHandler = new DownloadHandlerBuffer();
            req.SetRequestHeader("Content-Type", "application/json");
            yield return req.SendWebRequest();

            if(req.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Token Request Failed: " +req.error);
                yield break;
            }

            TokenResponse tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(req.downloadHandler.text);
            if(tokenResponse == null || string.IsNullOrEmpty(tokenResponse.server_url) || string.IsNullOrEmpty(tokenResponse.participant_token))
            {
                Debug.LogError("Invalid token Response");
                yield break;
            }
            var options = new RoomOptions();
            var connect = room.Connect(tokenResponse.server_url, tokenResponse.participant_token, options);
            yield return connect;
            if(!connect.IsError)
            {
                Debug.Log("Successfully Connected to " + room.Name);
            }
        }
        StartCoroutine(publishMicrophone());
        room.DataReceived += (data, participant, kind, topic) =>
        {
            if (topic != "emotion")
                return;
                
            var json = Encoding.UTF8.GetString(data);
            Debug.Log("Emotion packet received: " + json);

            var emotionData = JsonUtility.FromJson<EmotionMessage>(json);
            Debug.Log("Current Emotion: " + emotionData.emotion);

            anim = wilhelm_tell.GetComponent<Animator>();
            if (emotionData.emotion == "Angry")
            {
                anim.SetBool("isAngry", true);
            }
            else
            {
                anim.SetBool("isAngry", false);
            }
        };
    }

    void TrackSubscribed(IRemoteTrack track, RemoteTrackPublication publication, RemoteParticipant participant)
    {
        if(track is RemoteAudioTrack audioTrack)
        {
            if(wilhelm_body.GetComponent<AudioSource>() == null)
            {
                var source = wilhelm_body.AddComponent<AudioSource>();
                var stream = new AudioStream(audioTrack, source);
            }
            else
            {
                var source = wilhelm_body.GetComponent<AudioSource>();
                var stream = new AudioStream(audioTrack, source);
            }
        }
        
    }

    public IEnumerator publishMicrophone()
    {
        var localSid = "mic";
        string selectMic = "";
        GameObject audObject = new GameObject(localSid);
        _audioObjects[localSid] = audObject;
        foreach (var device in Microphone.devices)
        {
            Debug.Log("Found mic: " + device);
            if (device.Contains("Android"))
            {
                selectMic = device;
            }
        }
        if (selectMic == "")
        {
            Debug.Log("MIKROFON WIRD VERWENDET:");
            Debug.Log(Microphone.devices[0]);
            rtcSource = new MicrophoneSource(Microphone.devices[0], audObject);
        }
        else
        {
            Debug.Log("MIKROFON WIRD VERWENDET: SEL");
            Debug.Log(selectMic);
            rtcSource = new MicrophoneSource(selectMic, audObject);
        }

        var track = LocalAudioTrack.CreateAudioTrack("my-audio-track", rtcSource, room);
        var options = new TrackPublishOptions();
        options.AudioEncoding = new AudioEncoding();
        options.AudioEncoding.MaxBitrate = 64000;
        options.Source = TrackSource.SourceMicrophone;

        var publish = room.LocalParticipant.PublishTrack(track, options);
        yield return publish;

        if(!publish.IsError)
        {
            Debug.Log("Track published!");
        }

        _rtcAudioSources.Add(rtcSource);
        rtcSource.Start();
    }


    void onApplicationQuit()
    {
        Cleanup();
    }

    void onParticipantAttributesChanged(Participant participant)
    {
        if(participant.Attributes.TryGetValue("user_is_speaking", out var value))
        {
            Debug.Log("Value");
            Debug.Log(value);
            bool user_is_speaking = value == "true";
            if (user_is_speaking)
            {
                anim.SetBool("User_is_Speaking", true);
                Debug.Log("User is Speaking");
            }
            else
            {
                anim.SetBool("User_is_Speaking", false);
                Debug.Log("User is not Speaking");
            }
        }
    }

    Task Cleanup()
    {
        if(room!=null)
        {
            room.Disconnect();
        }
        foreach(var src in _rtcAudioSources)
        {
            src.Stop();
        }

        _rtcAudioSources.Clear();
        _audioObjects.Clear();

        return Task.CompletedTask;
    }
}

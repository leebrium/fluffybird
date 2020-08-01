using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;

public class FacebookManager : MonoBehaviour
{
    private static FacebookManager instance;
    public static FacebookManager Instance { get { return instance; } }
    void Awake ()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        } else {
            instance = this;
            DontDestroyOnLoad(this);
        }

        if (FB.IsInitialized) {
            FB.ActivateApp();
        } else {
            FB.Init( () => {
            FB.ActivateApp();
            });
        }
    }

    // Unity will call OnApplicationPause(false) when an app is resumed
    // from the background
    void OnApplicationPause (bool pauseStatus)
    {
        // Check the pauseStatus to see if we are in the foreground
        // or background
        if (!pauseStatus) {
            //app resume
            if (FB.IsInitialized) {
                FB.ActivateApp();
            } else {
            //Handle FB.Init
                FB.Init( () => {
                    FB.ActivateApp();
                });
            }
        }
    }
}

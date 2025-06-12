using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.Play.AppUpdate;
using Google.Play.Common;

public class UpdateManager : MonoBehaviour
{
    AppUpdateManager appUpdateManager = null;

    private void Awake()
    {

        appUpdateManager = new AppUpdateManager();
        StartCoroutine(CheckForUpdate());

    }


    IEnumerator CheckForUpdate()
    {
        Debug.Log("start CheckForUpdate");
        PlayAsyncOperation<AppUpdateInfo, AppUpdateErrorCode> appUpdateInfoOperation =
          appUpdateManager.GetAppUpdateInfo();

        // Wait until the asynchronous operation completes.
        yield return appUpdateInfoOperation;
        Debug.Log("start IsSuccessful1");
        if (appUpdateInfoOperation.IsSuccessful)
        {
            Debug.Log("start IsSuccessful2");
            var appUpdateInfoResult = appUpdateInfoOperation.GetResult();
            // Check AppUpdateInfo's UpdateAvailability, UpdatePriority,
            // IsUpdateTypeAllowed(), etc. and decide whether to ask the user
            // to start an in-app update.

            var appUpdateOptions = AppUpdateOptions.ImmediateAppUpdateOptions();

            //즉시 업데이트 처리
            //최신 AppUpdateInfo 객체와 올바르게 구성된 AppUpdateOptions 객체를 확보한 후
            //AppUpdateManager.StartUpdate()를 호출하여 비동기식으로 업데이트 흐름을 요청할 수 있습니다.
            StartCoroutine(StartImmediateUpdate(appUpdateManager, appUpdateInfoResult, appUpdateOptions));


        }
        else
        {
            Debug.Log("appUpdateInfoOperation.Error:" + appUpdateInfoOperation.Error);
        }

    }


    IEnumerator StartImmediateUpdate(AppUpdateManager appUpdateManager, AppUpdateInfo appUpdateInfoResult, AppUpdateOptions appUpdateOptions)
    {
        // Creates an AppUpdateRequest that can be used to monitor the
        // requested in-app update flow.
        Debug.Log("start StartImmediateUpdate");
        var startUpdateRequest = appUpdateManager.StartUpdate(
          // The result returned by PlayAsyncOperation.GetResult().
          appUpdateInfoResult,
          // The AppUpdateOptions created defining the requested in-app update
          // and its parameters.
          appUpdateOptions);
        yield return startUpdateRequest;

        // If the update completes successfully, then the app restarts and this line
        // is never reached. If this line is reached, then handle the failure (for
        // example, by logging result.Error or by displaying a message to the user).

    }
}

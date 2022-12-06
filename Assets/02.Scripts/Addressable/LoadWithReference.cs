using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
internal class LoadWithReference : MonoBehaviour
{
    // 에디터에서 할당
    public string adress;
    public Image image;
    private AsyncOperationHandle<Sprite> handle;

    void Start()
    {
        image = GetComponent<Image>();
        AsyncOperationHandle<Sprite> handle = Addressables.LoadAssetAsync<Sprite>(adress);
        handle.Completed += Handle_Completed;
    }

    
    private void Handle_Completed(AsyncOperationHandle<Sprite> operation)
    {

        // 완료 시 로드된 프리팹을 인스턴스화
        if (operation.Status == AsyncOperationStatus.Succeeded)
        {
            image.sprite = operation.Result;
            image.SetNativeSize();
        }
        else
        {
            Debug.LogError($"AssetReference {adress} failed to load.");
        }
    }

    private void OnDestroy()
    {

        // 상위 객체가 소멸되면 자산 해제
        Addressables.Release(handle);
    }
}
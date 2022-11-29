using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
internal class LoadWithReference : MonoBehaviour
{
    // �����Ϳ��� �Ҵ�
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

        // �Ϸ� �� �ε�� �������� �ν��Ͻ�ȭ
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

        // ���� ��ü�� �Ҹ�Ǹ� �ڻ� ����
        Addressables.Release(handle);
    }
}
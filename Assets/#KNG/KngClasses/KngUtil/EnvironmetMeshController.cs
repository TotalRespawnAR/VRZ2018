using UnityEngine;

public class EnvironmetMeshController : MonoBehaviour
{


    private void OnEnable()
    {
        // GameEventsManager.On_MatChange += UseOcclusion;
    }
    private void OnDisable()
    {
        // GameEventsManager.On_MatChange -= UseOcclusion;
    }

    public GameObject RefWallMobileDiff;
    Renderer RefWallMatMobileDiffRen;

    public GameObject RefWallMatSTD;
    Renderer RefWallMatSTDRen;


    public GameObject RefOcclusion;
    Renderer RefOcclusionRen;

    public GameObject RefOcclusionOpti;
    Renderer RefOcclusionRenOptiRen;

    public GameObject RefWindowOcclusion;
    Renderer RefWindowOcclusionRen;

    public GameObject RefLinOcclusion;
    Renderer RefLinOcclusionRen;

    public GameObject RefColiseum;
    Renderer RefColiseumRen;

    public GameObject RefTunnelVisible;
    Renderer RefTunnelVisibleRen;

    public GameObject RefLand;
    Renderer RefLandRen;

    public GameObject RefTunnels;
    Renderer RefTunnelsRen;

    public GameObject RefDoorLeft;
    Renderer RefDoorLeftRen;

    public GameObject RefDoorRight;
    Renderer RefDoorRightRen;



    // Use this for initialization
    void Start()
    {
        RefOcclusionRen = RefOcclusion.GetComponent<Renderer>();
        RefOcclusionRenOptiRen = RefOcclusionOpti.GetComponent<Renderer>();
        RefWindowOcclusionRen = RefWindowOcclusion.GetComponent<Renderer>();
        RefLinOcclusionRen = RefLinOcclusion.GetComponent<Renderer>();
        RefColiseumRen = RefColiseum.GetComponent<Renderer>();
        RefTunnelVisibleRen = RefTunnelVisible.GetComponent<Renderer>();
        RefLandRen = RefLand.GetComponent<Renderer>();
        RefTunnelsRen = RefTunnels.GetComponent<Renderer>();
        RefDoorLeftRen = RefDoorLeft.GetComponent<Renderer>();
        RefDoorRightRen = RefDoorRight.GetComponent<Renderer>();
        RefWallMatMobileDiffRen = RefWallMobileDiff.GetComponent<Renderer>();
        RefWallMatSTDRen = RefWallMatSTD.GetComponent<Renderer>();

    }

    void SetLand_tunnels_doorsOcclusion(Material argmat)
    {
        RefLandRen.enabled = true;
        RefTunnelsRen.enabled = true;
        RefDoorLeftRen.enabled = true;
        RefDoorRightRen.enabled = true;

        RefLandRen.material = argmat;
        RefTunnelsRen.material = argmat;
        RefDoorLeftRen.material = argmat;
        RefDoorRightRen.material = argmat;
    }
    void SetColiseum_vvisibleunnel(Material argmat)
    {
        GameManager.Instance.ShowGameMists();
        RefColiseumRen.enabled = true;
        RefTunnelVisibleRen.enabled = true;

        RefColiseumRen.material = argmat;
        RefTunnelVisibleRen.material = argmat;
    }

    void NoMeshNoMist()
    {
        RefLandRen.enabled = false;
        RefTunnelsRen.enabled = false;
        RefDoorLeftRen.enabled = false;
        RefDoorRightRen.enabled = false;
        RefColiseumRen.enabled = false;
        RefTunnelVisibleRen.enabled = false;
        GameManager.Instance.HIdeGameMists();
    }

    void UseOcclusion(int x)
    {
        switch (x)
        {
            case 0:
                NoMeshNoMist();
                break;
            case 1:
                SetLand_tunnels_doorsOcclusion(RefOcclusionRen.material);
                break;
            case 2:
                SetLand_tunnels_doorsOcclusion(RefOcclusionRenOptiRen.material);
                break;
            case 3:
                SetLand_tunnels_doorsOcclusion(RefWindowOcclusionRen.material);
                break;
            case 4:
                SetLand_tunnels_doorsOcclusion(RefLinOcclusionRen.material);
                break;
            case 5:
                SetColiseum_vvisibleunnel(RefWallMatMobileDiffRen.material);
                break;
            case 6:
                SetColiseum_vvisibleunnel(RefWallMatSTDRen.material);
                break;

        }
    }
}

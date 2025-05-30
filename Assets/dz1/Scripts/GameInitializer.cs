using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    [Header("Layer Masks")]
    [SerializeField] private LayerMask _grabbableLayer;
    [SerializeField] private LayerMask _explodableLayer;
    [SerializeField] private LayerMask _groundLayer;

    [Header("Visuals")]
    [SerializeField] private ParticleSystem _explosionEffect;

    private InputController _inputController;

    private void Start()
    {
        RaycastService raycastService = new RaycastService();

        GrabService grabService = new GrabService(
            raycastService,
            _grabbableLayer,
            _groundLayer);

        ExplosionService explosionService = new ExplosionService(
            raycastService,
            _explodableLayer,
            _groundLayer,
            _explosionEffect);

        _inputController = new InputController(grabService, explosionService);
    }

    private void Update()
    {
        _inputController.Update();
    }
}
using UnityEngine;

public class InputController
{
    private readonly GrabService _grabService;
    private readonly ExplosionService _explosionService;

    private int _leftMouseButton = 0;
    private int _rightMouseButton = 1;

    public InputController(GrabService grabService, ExplosionService explosionService)
    {
        _grabService = grabService;
        _explosionService = explosionService;
    }

    public void Update()
    {
        HandleMouseInput();
    }

    private void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(_leftMouseButton))
        {
            _grabService.TryStartDrag(Input.mousePosition);
        }

        if (Input.GetMouseButton(_leftMouseButton))
        {
            _grabService.UpdateDrag(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(_leftMouseButton))
        {
            _grabService.EndDrag();
        }

        if (Input.GetMouseButtonDown(_rightMouseButton))
        {
            _explosionService.TryCreateExplosion(Input.mousePosition);
        }
    }
}
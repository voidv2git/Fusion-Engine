using FusionEngine.Engine;
using FusionEngine.Engine.Rendering;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Input;

namespace FusionEngine
{
    public class MyGame : Game
    {
        public MyGame() : base(new Vector(750, 750), "Fusion Engine Test") { }

        private Vector moveDirection;

        private Entity player = new Entity(new Vector(), new Vector(50, 50), 0, Color.FromArgb(0,0,0));
        private Entity text = new Entity(new Vector(), new Vector(), 0, new Engine.Font("Test Text", new System.Drawing.Font(FontFamily.GenericMonospace, 20, FontStyle.Bold), Color.FromArgb(255,0,0)));

        private float angleMoveDirection;
        private const float PLAYER_MOVE_SPEED = 250;
        private const float PLAYER_ROTATE_SPEED = 180;

        public override void OnLoad()
        {

        }

        public override void OnUpdate()
        {
            HandleInput();
            MovePlayer();
        }

        private void MovePlayer()
        {
            player.position.x += moveDirection.x * Time.deltaTime * PLAYER_MOVE_SPEED;
            player.position.y += moveDirection.y * Time.deltaTime * PLAYER_MOVE_SPEED;
            player.angle += angleMoveDirection * Time.deltaTime * PLAYER_ROTATE_SPEED;

            text.angle += (float)Math.Sin(Time.time) / 10;
        }

        private void HandleInput()
        {
            moveDirection = new Vector();
            angleMoveDirection = 0;

            if (Input.GetKeyDown(Key.W)) moveDirection.y = -1;
            if (Input.GetKeyDown(Key.S)) moveDirection.y = 1;
            if (Input.GetKeyDown(Key.A)) moveDirection.x = -1;
            if (Input.GetKeyDown(Key.D)) moveDirection.x = 1;
            if (Input.GetKeyDown(Key.Q)) angleMoveDirection = -1;
            if (Input.GetKeyDown(Key.E)) angleMoveDirection = +1;
            if (Input.GetKeyDown(Key.Escape)) Application.Exit();

            moveDirection = moveDirection.normalize;
        }
    }
}

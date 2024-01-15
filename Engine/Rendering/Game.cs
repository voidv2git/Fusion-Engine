using System.Threading;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System;
using System.Diagnostics;

namespace FusionEngine.Engine.Rendering
{
    public abstract class Game
    {
        public Vector windowSize;
        public string title;
        public Window window;
        private Thread gameLoopThread;
        public static List<Entity> entityStack = new List<Entity>();

        public Game(Vector windowSize, string title)
        {
            this.windowSize = windowSize;
            this.title = title;

            window = new Window();
            window.Size = new Size((int)windowSize.x, (int)windowSize.y);
            window.Text = title;
            window.Paint += Renderer;

            gameLoopThread = new Thread(GameLoop);
            gameLoopThread.SetApartmentState(ApartmentState.STA);
            gameLoopThread.Start();

            Application.Run(window);
        }

        public static void RegisterEntity(Entity entity)
        {
            if (entity != null) entityStack.Add(entity);
        }

        private void UpdateComponents()
        {
            foreach (Entity entity in entityStack)
            {
                foreach (Component component in entity.components)
                {
                    component.OnUpdate();
                }
            }
        }

        private void GameLoop()
        {
            OnLoad();
            while (true)
            {
                try
                {
                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    Thread.Sleep(1);
                    sw.Stop();

                    Time.deltaTime = (float)sw.Elapsed.TotalSeconds;
                    Time.time += Time.deltaTime;

                    window.BeginInvoke((MethodInvoker)delegate { window.Refresh(); });

                    UpdateComponents();
                    OnUpdate();
                }
                catch (Exception) { }
            }
        }

        private void Renderer(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Color.White);
            foreach (Entity entity in entityStack)
            {
                if (entity.material is Color) g = DrawRectangle(entity, g);
                else if (entity.material is Image) g = DrawImage(entity, g);
                else if (entity.material is Font) g = DrawFont(entity, g);
            }
        }

        private Graphics DrawRectangle(Entity entity, Graphics g)
        {
            g.TranslateTransform((int)entity.position.x, (int)entity.position.y);
            g.RotateTransform(entity.angle);

            g.FillRectangle(new SolidBrush((Color)entity.material), -(int)entity.scale.x / 2, -(int)entity.scale.y / 2, (int)entity.scale.x, (int)entity.scale.y);

            g.RotateTransform(-entity.angle);
            g.TranslateTransform(-(int)entity.position.x, -(int)entity.position.y);

            return g;
        }

        private Graphics DrawImage(Entity entity, Graphics g)
        {
            g.TranslateTransform((int)entity.position.x, (int)entity.position.y);
            g.RotateTransform(entity.angle);

            g.DrawImageUnscaledAndClipped((Image)entity.material, new Rectangle(-(int)entity.scale.x / 2, -(int)entity.scale.y / 2, (int)entity.scale.x, (int)entity.scale.y));

            g.RotateTransform(-entity.angle);
            g.TranslateTransform(-(int)entity.position.x, -(int)entity.position.y);

            return g;
        }

        private Graphics DrawFont(Entity entity, Graphics g)
        {
            g.TranslateTransform((int)entity.position.x, (int)entity.position.y);
            g.RotateTransform(entity.angle);

            Font font = (Font)entity.material;
            g.DrawString(font.value, font.font, new SolidBrush(font.color), -(int)entity.scale.x / 2, -(int)entity.scale.y / 2);

            g.RotateTransform(-entity.angle);
            g.TranslateTransform(-(int)entity.position.x, -(int)entity.position.y);

            return g;
        }

        public abstract void OnLoad();
        public abstract void OnUpdate();
    }
}

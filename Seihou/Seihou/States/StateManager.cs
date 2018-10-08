using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Seihou
{
    public abstract class State
    {
        public readonly StateManager sm;
        public readonly GraphicsDeviceManager gdm;
        public readonly SpriteBatch sb;
        public readonly ContentManager cm;

        public State(StateManager sm, ContentManager cm, SpriteBatch sb, GraphicsDeviceManager gdm)
        {
            this.sm = sm;
            this.sb = sb;
            this.cm = cm;
            this.gdm = gdm;
        }

        public abstract void Update(GameTime gt);
        public abstract void Draw(GameTime gt);
        public virtual void OnExit() { }
        public virtual void OnStart() { }
    }

    public class StateManager
    {
        //Game
        public bool abort = false;

        //State
        private readonly Dictionary<string,State> states = new Dictionary<string,State>();
        private State currentState = null;
        private State pollState = null;

        //FPS
        private readonly Queue<float> fpsMeasure = new Queue<float>();
        private const int fpsSampleSize = 20;

        public State GetCurrentState() => currentState;

        public float GetFps() => fpsMeasure.Average();

        public void ChangeState(State s) => pollState = s;

        public void Update(GameTime gt)
        {
            if (pollState != null)
            {
                //Let the old state know he is about to die
                if (currentState != null) currentState.OnExit();

                //Start the new state
                currentState = pollState;
                currentState.OnStart();
                
                //Clear buffer/poll
                pollState = null;
            }
            currentState.Update(gt);
        }

        public void Draw(GameTime gt)
        {
            fpsMeasure.Enqueue((float)(1.0 / gt.ElapsedGameTime.TotalSeconds));
            if (fpsMeasure.Count >= fpsSampleSize) fpsMeasure.Dequeue();

            currentState.Draw(gt);
        }
    }
}


//State template
/*
    class **StateName** : State
    {
        public **StateName**(StateManager sm, ContentManager cm, SpriteBatch sb, GraphicsDeviceManager gdm) : base(sm,cm,sb,gdm)
        {
        }

        public override void Draw(GameTime gt)
        {
        }

        public override void Update(GameTime gt)
        {
        }

        public override void OnStart()
        {
        }

        public override void OnExit()
        {
        }
    }  
*/

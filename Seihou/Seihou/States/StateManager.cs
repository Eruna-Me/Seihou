using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        private float averageFps = 0f;
        private const float updateFpsInterval = 0.3f;
        private float updateFpsTimer = 0f;
        private const int maxSampleSize = 10;

        public State GetCurrentState() => currentState;

        public float GetFps() => averageFps;

        public void ChangeState(State s) => pollState = s;

        public void Update(GameTime gt)
        {
            float addFrame = (float)(1.0 / gt.ElapsedGameTime.TotalSeconds);
            fpsMeasure.Enqueue(float.IsInfinity(addFrame) ? 0 : addFrame);
            if (fpsMeasure.Count >= maxSampleSize) fpsMeasure.Dequeue();

            updateFpsTimer += gt.Time();
            if (updateFpsTimer > updateFpsInterval)
            {
                averageFps = (float)Math.Round(fpsMeasure.Average(),1);
                updateFpsTimer = 0;
            }

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

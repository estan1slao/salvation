using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Salvation_v2
{
    public class NTimer
    {
        public bool complete;
        protected int mSec;
        protected TimeSpan timer = new TimeSpan();

        public NTimer(int m)
        {
            complete = false;
            mSec = m;
        }
        public NTimer(int m, bool STARTLOADED)
        {
            complete = STARTLOADED;
            mSec = m;
        }

        public int MSec
        {
            get { return mSec; }
            set { mSec = value; }
        }
        public int Timer
        {
            get { return (int)timer.TotalMilliseconds; }
        }

        public void UpdateTimer() => timer += Globals.GameTime.ElapsedGameTime;

        public void UpdateTimer(float SPEED) => timer += TimeSpan.FromTicks((long)(Globals.GameTime.ElapsedGameTime.Ticks * SPEED));

        public virtual void AddToTimer(int MSEC) => timer += TimeSpan.FromMilliseconds((long)(MSEC));

        public bool Test()
        {
            if(timer.TotalMilliseconds >= mSec || complete)
                return true;
            else
                return false;
        }

        public void Reset()
        {
            timer = timer.Subtract(new TimeSpan(0, 0, mSec/60000, mSec/1000, mSec%1000));
            if(timer.TotalMilliseconds < 0)
                timer = TimeSpan.Zero;
            complete = false;
        }

        public void Reset(int NEWTIMER)
        {
            timer = TimeSpan.Zero;
            MSec = NEWTIMER;
            complete = false;
        }

        public void ResetToZero()
        {
            timer = TimeSpan.Zero;
            complete = false;
        }

        public void SetTimer(TimeSpan TIME) => timer = TIME;

        public virtual void SetTimer(int MSEC) => timer = TimeSpan.FromMilliseconds((long)(MSEC));
    }
}

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
        private bool Complete;
        protected int mSec;
        protected TimeSpan timer = new TimeSpan();

        public int MSec
        {
            get { return mSec; }
            set { mSec = value; }
        }
        public int Timer
        {
            get { return (int)timer.TotalMilliseconds; }
        }

        public NTimer(int mSec)
        {
            Complete = false;
            this.mSec = mSec;
        }
        public NTimer(int mSec, bool startLoaded)
        {
            Complete = startLoaded;
            this.mSec = mSec;
        }

        public void UpdateTimer() => timer += Globals.GameTime.ElapsedGameTime;

        public void UpdateTimer(float speed) => timer += TimeSpan.FromTicks((long)(Globals.GameTime.ElapsedGameTime.Ticks * speed));

        public virtual void AddToTimer(int mSec) => timer += TimeSpan.FromMilliseconds((long)(mSec));

        public bool Test()
        {
            if(timer.TotalMilliseconds >= mSec || Complete)
                return true;
            else
                return false;
        }

        public void Reset()
        {
            timer = timer.Subtract(new TimeSpan(0, 0, mSec/60000, mSec/1000, mSec%1000));
            if(timer.TotalMilliseconds < 0)
                timer = TimeSpan.Zero;
            Complete = false;
        }

        public void Reset(int newTimer)
        {
            timer = TimeSpan.Zero;
            MSec = newTimer;
            Complete = false;
        }

        public void ResetToZero()
        {
            timer = TimeSpan.Zero;
            Complete = false;
        }

        public void SetTimer(TimeSpan time) => timer = time;

        public virtual void SetTimer(int mSec) => timer = TimeSpan.FromMilliseconds((long)(mSec));
    }
}

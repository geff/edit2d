using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Reflection;
using Microsoft.Xna.Framework.Audio;
using System.ComponentModel;

namespace Edit2DEngine.Action
{
    public class ActionSound : ActionBase
    {
        [Browsable(true)]
        public String SoundName {get;set;}
        [Browsable(true)]
        public Boolean Loop { get; set; }

        public ActionSound(Script script, string actionName, string soundName, bool loop)
        {
            this.Script = script;
            this.ActionName = actionName;

            this.SoundName = soundName;
            this.Loop = loop;
        }

        public override void InitAction()
        {
        }

        public void PlaySound()
        {
            //Cue c;
        }
    }
}

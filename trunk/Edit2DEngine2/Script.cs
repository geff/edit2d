using System;
using System.Collections.Generic;
using System.Text;
using Edit2DEngine.Action;

namespace Edit2DEngine
{
    public class Script
    {
        public List<ActionBase> ListAction { get; set; }
        
        //public Entite Entite { get; set; }
        public Object ActionHandler { get; set; }
        public String ScriptName { get; set; }

        public Script(string scriptName, Object actionHandler)
        {
            this.ScriptName = scriptName;
            //this.Entite = entite;
            this.ActionHandler = actionHandler;
            this.ListAction = new List<ActionBase>();
        }

        public void StartScript(Repository repository)
        {
            foreach (ActionBase action in ListAction)
            {
                if (action is ActionCurve)
                {
                    //if (((ActionCurve)action).playAnimation != 0)
                    //    ((ActionCurve)action).playAnimation = 0;
                    //else
                    if (((ActionCurve)action).playAnimationState == PlayAnimationState.Stop ||
                        ((ActionCurve)action).playAnimationState == PlayAnimationState.PlayManually)
                    {
                        ((ActionCurve)action).StartAnimation(PlayAnimationState.Play);
                    }
                }
                else if (action is ActionEvent)
                {
                    ((ActionEvent)action).Playing = true;
                    //((ActionEvent)action).UpdateValue(repository);
                }
            }
        }
    }
}

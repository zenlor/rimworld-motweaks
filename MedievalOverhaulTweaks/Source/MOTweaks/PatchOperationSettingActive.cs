using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

using Verse;

namespace MOTweaks
{
    public class PatchOperationSettingActive: PatchOperation
    {
        public List<string> settings;
        public PatchOperation active;
        public PatchOperation inactive;

        protected override bool ApplyWorker(XmlDocument xml)
        {
            string s = settings.First();

            if (MOTweaksMod.settings.GetEnabledSettings.Contains(s) && active != null)
            {
                Log.Message("[MOTweaks] patch '" + s + "' active");
                return active.Apply(xml);
            }
            else if (inactive != null)
            {
                return inactive.Apply(xml);
            }

            return true;
        }
    }
}

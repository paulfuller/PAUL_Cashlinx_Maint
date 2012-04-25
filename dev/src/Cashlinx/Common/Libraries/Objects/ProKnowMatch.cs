/************************************************************************
 * Namespace:       PawnObjects.Pawn.Includes
 * Class:           ProKnowMatch
 * 
 * Description      The class keeps the information of a ProKnow Match
 *                  during a ProKnow Lookup
 * 
 * History
 * David D Wise, 4-07-2009, Initial Development
 * 
 * **********************************************************************/

using System.Collections.Generic;
using Common.Libraries.Utility.Shared;

namespace Common.Libraries.Objects
{
    public class ProKnowMatch
    {
        public List<Answer>         manufacturerModelInfo           {get; set;} //
        public bool                 displayFixedFeaturesOnTag       {get; set;} //
        public List<FixedFeature>   fixedFeaturesList               {get; set;} //
        public List<Answer>         preAnsweredQuestions            {get; set;} //
        public int                  primaryCategoryCode             {get; set;} //
        public string               primaryCategoryCodeDescription  {get; set;} //
        public int                  primaryMaskPointer              {get; set;} //
        public ProCallData          proCallData                     {get; set;} //
        public List<ProKnowData>    proKnowData                     {get; set;} //
        public ProKnowData          selectedPKData                  {get; set;} //
        public TransitionStatus     transitionStatus                {get; set;} //
    }
}

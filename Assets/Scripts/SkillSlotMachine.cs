using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlotMachine : MonoBehaviour
{
    [Serializable]
    struct SkillSet
    {
        public Transform SkillSetTransform;
        public List<Image> Images;

        public SkillSet(Transform skillSetTransform, List<Image> images)
        {
            SkillSetTransform = skillSetTransform;
            Images = images;
        }
    }

    [SerializeField] private List<SkillSet> skillSets = new List<SkillSet>();
}

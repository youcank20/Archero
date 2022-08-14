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
    }

    [SerializeField] private List<SkillSet> skillSets = new List<SkillSet>();
}

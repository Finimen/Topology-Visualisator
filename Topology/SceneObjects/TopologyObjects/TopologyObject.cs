using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Assets.Scripts.Topology
{
    public abstract class TopologyObject : MonoBehaviour, ISceneObject
    {
        [SerializeField] protected Transform variablesPosition;
        [SerializeField] protected Transform methodsPosition;

        [SerializeField] protected float offestY;

        [SerializeField] protected Image blackgroundImage;

        [SerializeField] protected Text typeText;
        [SerializeField] protected Text nameText;

        [SerializeField] protected new string name;

        [SerializeField] protected Color blackgroundColor;

        [SerializeField] protected List<Variable> variables;

        [SerializeField] protected List<Method> methods;

        public abstract List<Variable> Variables
        { 
            get;
            set;
        }
        
        public abstract List<Method> Methods
        {
            get;
            set;
        }
 
        public Color BlackgroundColor
        {
            get
            {
                return blackgroundColor;
            }
            set
            {
                blackgroundColor = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
        }

        public void Rename(string name)
        {
            this.name = name;

            UnityEngine.Debug.Log(";D");
        }

        public void AddVariable(Variable variable)
        {
            variables.Add(variable);
        }

        public void AddMethod(Method method)
        {
            methods.Add(method);
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }

        public void SetScale(float scale)
        {
            transform.localScale = Vector3.one * scale;
        }

        public abstract void SelectStateVariables();

        public abstract void SelectStateMethods();

        public abstract bool VariablesUnchanged();
        public abstract bool MethodsUnchanged();
    }
}
using System.Threading.Tasks;
using UnityEngine;

namespace Infrastructure.Initialization
{
    public abstract class AsyncInitialization : MonoBehaviour
    {
        public abstract Task InitializeAsync();
    }
}
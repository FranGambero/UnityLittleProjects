using UnityEngine;

/// <summary>
/// Class used to simulate singleto behaviour
/// </summary>
/// <typeparam name="T">Class to be singleted!</typeparam>
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour {
    /// <summary>
    /// Objtect Instance
    /// </summary>
    protected static T _Instance;

    /// <summary>
    /// Get Instance
    /// </summary>
    public static T Instance {
        get {
            if (_Instance == null) {
                _Instance = (T)FindObjectOfType(typeof(T));
            }
            return _Instance;
        }
    }
}

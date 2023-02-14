using UnityEngine;

public class MagazinesDropBox : MonoBehaviour
{
    private int _magazinesCount;
    
    public Weapon.Weapon Weapon { get; private set; }

    public void Init(int magazinesCount, Weapon.Weapon weapon)
    {
        _magazinesCount = magazinesCount;
        Weapon = weapon;
    }

    public void Unpack()
    {
        Weapon.AddMagazinesCount(_magazinesCount);
    }
}

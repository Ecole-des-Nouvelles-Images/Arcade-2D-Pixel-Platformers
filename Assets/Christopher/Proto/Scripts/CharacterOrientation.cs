using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterOrientation : MonoBehaviour
{
    public void SetOrientation(Vector2 joystickValue, Transform projectilSpawnerPosition, Vector2 orientationVew, Animator prefabAnimator)
    {
         switch (joystickValue.x )
                    {
                        case <-0.1f :
                            projectilSpawnerPosition.position = new Vector3(transform.position.x - 1, projectilSpawnerPosition.position.y, 0);
                            orientationVew.x = -1;
                            prefabAnimator.SetBool("moving horizontal",true);
                            break;
                        case >0.1f:
                            projectilSpawnerPosition.position = new Vector3(transform.position.x + 1, projectilSpawnerPosition.position.y, 0);
                            orientationVew.x = 1;
                            prefabAnimator.SetBool("moving horizontal",true);
                            break;
                        default:
                            projectilSpawnerPosition.position = new Vector3(transform.position.x, projectilSpawnerPosition.position.y, 0);
                            orientationVew.x = 0;
                            prefabAnimator.SetBool("moving horizontal",false);
                            break;
                    }
                    switch (joystickValue.y )
                    {
                        case <-0.1f :
                            projectilSpawnerPosition.position = new Vector3(projectilSpawnerPosition.position.x, transform.position.y-1, 0);
                            orientationVew.y = -1;
                            prefabAnimator.SetBool("moving down",true);
                            break;
                        case >0.1f:
                            projectilSpawnerPosition.position = new Vector3(projectilSpawnerPosition.position.x, transform.position.y+1, 0);
                            orientationVew.y = 1;
                            prefabAnimator.SetBool("moving up",true);
                            break;
                        default:
                            projectilSpawnerPosition.position = new Vector3(projectilSpawnerPosition.position.x, transform.position.y, 0);
                            orientationVew.y = 0;
                            prefabAnimator.SetBool("moving down",false);
                            prefabAnimator.SetBool("moving up",false);
                            break;
                    }
        
                    if (joystickValue.x == 0 && joystickValue.y == 0)
                    {
                        orientationVew.x = 1;
                    }
    }
}

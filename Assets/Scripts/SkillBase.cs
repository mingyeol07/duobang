using UnityEngine;

public class SkillBase : MonoBehaviour
{


    private float curCool;
    private float maxCool;

    public void UpdateLogic()
    {
        curCool += Time.deltaTime;
        if (curCool > maxCool)
        {
            curCool = 0;
            // 플레이어 큐에 넣기

        }
    }

    public void Action()
    {

    }
}

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
            // �÷��̾� ť�� �ֱ�

        }
    }

    public void Action()
    {

    }
}

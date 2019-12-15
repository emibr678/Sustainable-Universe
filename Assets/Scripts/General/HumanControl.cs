using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HumanControl : MonoBehaviour
{
    public Text idle_count_text;
    public Text lumberjack_count_text;
    public Text hunter_count_text;
    public Text wood_count;
    public Text food_count;
    
    public void RemoveLumberjack()
    {
        GameObject[] humans = GameObject.FindGameObjectsWithTag("Human");
        for(int i = 0; i < humans.Length; i++)
        {
            Human human = humans[i].GetComponent<Human>();
            if(human.job == HumanJob.Lumberjack)
            {
                Instantiate(Resources.Load("Prefabs/IdleHuman"), humans[i].transform.position, Quaternion.identity);
                Destroy(humans[i]);
                break;
            }
        }
    }
    
    public void AddLumberjack()
    {
        GameObject[] humans = GameObject.FindGameObjectsWithTag("Human");
        for(int i = 0; i < humans.Length; i++)
        {
            Human human = humans[i].GetComponent<Human>();
            if(human.job == HumanJob.Idle)
            {
                Instantiate(Resources.Load("Prefabs/Lumberjack"), humans[i].transform.position, Quaternion.identity);
                Destroy(humans[i]);
                break;
            }
        }
    }
    
    public void RemoveHunter()
    {
        GameObject[] humans = GameObject.FindGameObjectsWithTag("Human");
        for(int i = 0; i < humans.Length; i++)
        {
            Human human = humans[i].GetComponent<Human>();
            if(human.job == HumanJob.Hunter)
            {
                Instantiate(Resources.Load("Prefabs/IdleHuman"), humans[i].transform.position, Quaternion.identity);
                Destroy(humans[i]);
                break;
            }
        }
    }
    
    public void AddHunter()
    {
        GameObject[] humans = GameObject.FindGameObjectsWithTag("Human");
        for(int i = 0; i < humans.Length; i++)
        {
            Human human = humans[i].GetComponent<Human>();
            if(human.job == HumanJob.Idle)
            {
                Instantiate(Resources.Load("Prefabs/Hunter"), humans[i].transform.position, Quaternion.identity);
                Destroy(humans[i]);
                break;
            }
        }
    }
    
    void Update()
    {
        GameObject[] humans = GameObject.FindGameObjectsWithTag("Human");
        int idle_count       = 0;
        int lumberjack_count = 0;
        int hunter_count     = 0;
        
        for(int i = 0; i < humans.Length; i++)
        {
            Human human = humans[i].GetComponent<Human>();
            
            if(human.job == HumanJob.Idle)
            {
                idle_count++;
            }
            else if(human.job == HumanJob.Lumberjack)
            {
                lumberjack_count++;
            }
            else if(human.job == HumanJob.Hunter)
            {
                hunter_count++;
            }
        }
        
        idle_count_text.text       = idle_count.ToString();
        lumberjack_count_text.text = lumberjack_count.ToString();
        hunter_count_text.text     = hunter_count.ToString();
        
        CivBaseSim civ_base;
        civ_base = GameObject.FindGameObjectsWithTag("CivBase")[0].GetComponent<CivBaseSim>();
        wood_count.text            = civ_base.wood.ToString();
        food_count.text            = civ_base.food.ToString();
    }
}

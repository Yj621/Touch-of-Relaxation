using UnityEngine;
using UnityEngine.UI;
using System.Collections;
 
public class NoticeUI : MonoBehaviour
{
    [Header("SubNotice")]
    public GameObject noticebox;
    public Text noticeext;
    public Animator noticeani;
 
    // 코루틴 딜레이
    private WaitForSeconds _UIDelay1 = new WaitForSeconds(2.0f);
    private WaitForSeconds _UIDelay2 = new WaitForSeconds(0.3f);
 
    void Start()
    {
        noticebox.SetActive(false);
    }
 
    // 서브 메세지 >> string 값을 매개 변수로 받아와서 2초간 출력
    // 사용법 : _notice.SUB("문자열");
    public void SUB(string message)
    {
        noticeext.text = message;
        noticebox.SetActive(false);
        StopAllCoroutines();
        StartCoroutine(SUBDelay());
    }
 
    // 반복 되지 않게 하기 위해서 딜레이 설정
    IEnumerator SUBDelay()
    {
        noticebox.SetActive(true);
        noticeani.SetBool("isOn", true);
        yield return _UIDelay1;
        noticeani.SetBool("isOn", false);
        yield return _UIDelay2;
        noticebox.SetActive(false);
    }
}
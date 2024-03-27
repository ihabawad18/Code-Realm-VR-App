/*using UnityEditor;

[CustomEditor(typeof(QuestionsAndAnswers))]
public class QuestionsAndAnswersEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        QuestionsAndAnswers qa = (QuestionsAndAnswers)target;

        EditorGUILayout.LabelField("Question");
        qa.question = EditorGUILayout.TextField(qa.question);

        EditorGUILayout.LabelField("Answers");
        for (int i = 0; i < qa.answers.Length; i++)
        {
            qa.answers[i] = EditorGUILayout.TextField("Answer " + i, qa.answers[i]);
        }

        EditorGUILayout.LabelField("Correct Answer");
        qa.correctAnswer = EditorGUILayout.IntField(qa.correctAnswer);
    }
}*/
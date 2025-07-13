using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionNode : IDesitionNode
{
    IDesitionNode trueNode;
    IDesitionNode falseNode;

    private Func<bool> question;

    public QuestionNode(IDesitionNode trueNode, IDesitionNode falseNode, Func<bool> question)
    {
        this.trueNode = trueNode;
        this.falseNode = falseNode;
        this.question = question;
    }

    public void Execute() //Executes one of two nodes based on the given condition.
    {
        if (question())
        {
            trueNode.Execute();
        }
        else
        {
            falseNode.Execute();
        }
    }
}

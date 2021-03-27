using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;

public class CardHandTest
{
    public Card _card3;
    public Card _cardK;
    public Card _cardA;
    public CardHand _cardHand;

    [OneTimeSetUp]
    public void LoadCardsPrefabs()
    {
        _card3 = LoadCard("Assets/PlayingCards/Prefabs/2D/Clubs/Club_3.prefab");
        _cardK = LoadCard("Assets/PlayingCards/Prefabs/2D/Clubs/Club_K.prefab");
        _cardA = LoadCard("Assets/PlayingCards/Prefabs/2D/Clubs/Club_A.prefab");
    }

    private Card LoadCard(string path)
    {
        GameObject card = (GameObject)AssetDatabase.LoadAssetAtPath(path, typeof(GameObject));
        return card.GetComponent<Card>();
    }

    [SetUp]
    public void SetupCardHand()
    {
        GameObject cardHandGo = new GameObject();
        cardHandGo.AddComponent<CardHand>();
        _cardHand = cardHandGo.GetComponent<CardHand>();
    }

    [Test]
    public void TestSimpleSum()
    {
        _cardHand.AddCard(GameObject.Instantiate(_card3));
        _cardHand.AddCard(GameObject.Instantiate(_cardK));

        Assert.AreEqual(13, _cardHand.GetHandValue());
    }

    [Test]
    public void TestSimpleSumOver21()
    {
        _cardHand.AddCard(GameObject.Instantiate(_card3));
        _cardHand.AddCard(GameObject.Instantiate(_cardK));
        _cardHand.AddCard(GameObject.Instantiate(_cardK));

        Assert.AreEqual(23, _cardHand.GetHandValue());
    }

    [Test]
    public void TestBlackjack()
    {
        _cardHand.AddCard(GameObject.Instantiate(_cardA));
        _cardHand.AddCard(GameObject.Instantiate(_cardK));

        Assert.AreEqual(21, _cardHand.GetHandValue());
    }

    [Test]
    public void TestTwoAces_ShouldBe11and1()
    {
        _cardHand.AddCard(GameObject.Instantiate(_cardA));
        _cardHand.AddCard(GameObject.Instantiate(_cardA));
        _cardHand.AddCard(GameObject.Instantiate(_card3));

        Assert.AreEqual(15, _cardHand.GetHandValue());
    }

    [Test]
    public void TestTwoAces_ShouldBe1()
    {
        _cardHand.AddCard(GameObject.Instantiate(_cardA));
        _cardHand.AddCard(GameObject.Instantiate(_cardA));
        _cardHand.AddCard(GameObject.Instantiate(_cardK));

        Assert.AreEqual(12, _cardHand.GetHandValue());
    }

    [Test]
    public void TestThreeAces_ShouldBe11And1And1()
    {
        _cardHand.AddCard(GameObject.Instantiate(_cardA));
        _cardHand.AddCard(GameObject.Instantiate(_cardA));
        _cardHand.AddCard(GameObject.Instantiate(_cardA));
        _cardHand.AddCard(GameObject.Instantiate(_card3));

        Assert.AreEqual(16, _cardHand.GetHandValue());
    }
}

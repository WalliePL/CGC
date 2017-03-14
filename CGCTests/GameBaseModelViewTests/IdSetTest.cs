namespace CGCTests.GameBaseModelViewTests
{
    #region

    using System.Collections.Generic;

    using GameBaseModel.Api;
    using GameBaseModel.Api.Interfaces;
    using GameBaseModel.Player;

    using NUnit.Framework;

    #endregion

    [TestFixture]
    public class IdSetTests
    {
        #region Public Methods and Operators

        [TestCase]
        public void GetIdAfterAdding()
        {
            var idList = this.CreateIdList(1);

            var idSetter = new IdSetter(idList);
            Assert.AreEqual(idSetter.getId(), 1);
        }

        [TestCase]
        public void GetIdAfterPlacingSomeElementsInTheMiddle()
        {
            var idList = this.CreateIdList(30);

            idList.RemoveAt(14);
            idList.RemoveAt(14);
            idList.RemoveAt(14);

            this.AddToList(ref idList);
            this.AddToList(ref idList);

            var TestIdSetter = new IdSetter(idList);
            Assert.AreEqual(TestIdSetter.getId(), 16);
        }

        [TestCase]
        public void GetIdAfterRemovingFirst()
        {
            var idList = this.CreateIdList(30);

            idList.RemoveAt(0);

            var TestIdSetter = new IdSetter(idList);
            Assert.AreEqual(TestIdSetter.getId(), 0);
        }

        [TestCase]
        public void GetIdAfterRemovingInTheMiddleOfList()
        {
            var idList = this.CreateIdList(30);

            idList.RemoveAt(15);

            var TestIdSetter = new IdSetter(idList);
            Assert.AreEqual(TestIdSetter.getId(), 15);
        }

        [TestCase]
        public void GetIdAfterRemovingLast()
        {
            var idList = this.CreateIdList(20);
            idList.RemoveAt(19);

            var idSetter = new IdSetter(idList);
            Assert.AreEqual(idSetter.getId(), 19);
        }

        [TestCase]
        public void GetIdAfterRemovingSomeElementsInTheMiddle()
        {
            var idList = this.CreateIdList(30);

            idList.RemoveAt(14);
            idList.RemoveAt(14);
            idList.RemoveAt(14);

            var TestIdSetter = new IdSetter(idList);
            Assert.AreEqual(TestIdSetter.getId(), 14);
        }

        [TestCase]
        public void GetIdFromEmptyList()
        {
            var idList = new List<IId>();
            var idSetter = new IdSetter(idList);
            Assert.AreEqual(idSetter.getId(), 0);
        }

        #endregion

        #region Methods

        private void AddToList(ref List<IId> idList)
        {
            var player = new Player();
            var idSetter = new IdSetter(idList);
            player.Id = idSetter.getId();
            idList.Add(player);
        }

        private List<IId> CreateIdList(int HowMannyElements)
        {
            var idList = new List<IId>();

            for (int ListElement = 0; ListElement < HowMannyElements; ListElement++)
            {
                this.AddToList(ref idList);
            }

            return idList;
        }

        #endregion
    }
}
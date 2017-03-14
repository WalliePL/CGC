namespace GameBaseModel.Api
{
    #region

    using System.Collections.Generic;
    using System.Linq;

    using GameBaseModel.Api.Interfaces;

    #endregion

    public class IdSetter
    {
        #region Fields

        private readonly List<IId> list;

        #endregion

        #region Constructors and Destructors

        public IdSetter(List<IId> newList)
        {
            this.list = newList;
            this.list = this.list.OrderBy(o => o.Id).ToList();
        }

        #endregion

        #region Public Methods and Operators

        public int getId()
        {
            int idQty = this.list.Count();

            int idElement = 0;
            for (idElement = 0; idElement < idQty; ++idElement)
            {
                if (this.list[idElement].Id > idElement)
                {
                    return idElement;
                }
            }

            return idElement;
        }

        #endregion
    }
}
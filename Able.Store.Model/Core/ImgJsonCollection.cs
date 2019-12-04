using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
namespace Able.Store.Model.Core
{
    public class ImgJsonCollection : IEnumerable<ImgJson>
    {
        private string _imgAndDesc;
        public ImgJsonCollection(int? keyId, string imgAndDesc)
        {
            _imgAndDesc = imgAndDesc;
            if (!string.IsNullOrEmpty(_imgAndDesc))
            {
                this.KeyId = keyId;
                try
                {
                    SkuImgs = JsonConvert
                           .DeserializeObject<IList<ImgJson>>(imgAndDesc)
                           .OrderBy(x => x.Sort).ToList();
                }
                catch (Exception ex)
                {
                    //可以记录日志
                }
            }
        }
        private object _synch = new object();
        public int Count
        {
            get
            {
                return this.SkuImgs.Count;
            }
        }
        public int? KeyId { get; private set; }
        internal IList<ImgJson> SkuImgs
        {
            get; private set;
        }
        public ImgJson this[int index]
        {
            get
            {
                if (SkuImgs != null && SkuImgs.Count < index)
                    return SkuImgs[index];

                return null;
            }
        }

        public IEnumerator<ImgJson> GetEnumerator()
        {
            lock (_synch)
            {
                foreach (var item in SkuImgs)
                {
                    yield return item;
                }
            }
        }

        private int _currentIndex = 0;
        public ImgJson GetCurrentItem()
        {

            if (this.SkuImgs == null || this.SkuImgs.Count == 0)
                return default(ImgJson);

            lock (_synch)
            {
                if (_currentIndex >= this.Count)
                    _currentIndex = this.Count - 1;
                    
                var item = SkuImgs[_currentIndex];

                _currentIndex++;

                return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

using System.Collections.Generic;

namespace CruPhysics.Utilities
{
    public class CollectionChangeInfo<TElement>
    {
        public IReadOnlyCollection<TElement> AddedElements { get; set; }
        public IReadOnlyCollection<TElement> RemovedElements { get; set; }
    }
}

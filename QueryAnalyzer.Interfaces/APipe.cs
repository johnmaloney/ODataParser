using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryAnalyzer.Interfaces
{
    public abstract class APipe
    {
        #region Fields

        private Queue<object> pipeline;

        #endregion

        #region Properties

        protected bool HasNextPipe { get { return pipeline.Count > 0; } }
        
        #endregion

        #region Methods

        public APipe Add<TPipe>(TPipe childPipe)
        {
            if (pipeline == null)
                pipeline = new Queue<object>();

            pipeline.Enqueue(childPipe);
            // Allows for Fluent additions of pipes to each other //
            // ex: 
            return this;
        }
        
        public TPipe Next<TPipe>()
        {
            if (pipeline.Count > 0)
                return (TPipe)pipeline.Dequeue();
            else
                return default(TPipe);
        }

        public APipe() { this.pipeline = new Queue<object>(); }

        #endregion
    }
}

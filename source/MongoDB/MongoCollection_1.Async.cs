using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace MongoDB
{
	public partial class MongoCollection<T> : IMongoCollection<T>
	{
        /// <summary>
        /// Finds the first document in a selector query and returns it through the callback delegate.
        /// </summary>
        /// <param name="javascriptWhere">The where.</param>
        /// <param name="returnDelegate">The callback action.</param>
        /// <returns>A <see cref="CancellationTokenSource"/> for cancelling the task.</returns>
        public CancellationTokenSource FindOneAsync(string javascriptWhere, Action<T, Exception> returnDelegate)
        {
            var cancel = new CancellationTokenSource();
            new TaskFactory().StartNew(() => 
                {
                    if (!cancel.Token.IsCancellationRequested)
                    {
                        try
                        {
                            var t = FindOne(javascriptWhere);
                            returnDelegate.Invoke(t, null);
                        }
                        catch (Exception ex)
                        {
                            returnDelegate.Invoke(null, ex);
                        }
                    }
                },cancel.Token);
            return cancel;
        }

        /// <summary>
        /// Finds the first document in a selector query and returns it through the callback delegate.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="returnDelegate">The callback action</param>
        /// <returns>A <see cref="CancellationTokenSource"/> for cancelling the task.</returns>
        public CancellationTokenSource FindOneAsync(object selector, Action<T, Exception> returnDelegate)
        {
            var cancel = new CancellationTokenSource();
            
            new TaskFactory().StartNew(() =>
            {
                if (!cancel.Token.IsCancellationRequested)
                {
                    try
                    {
                        var t = FindOne(selector);
                        returnDelegate.Invoke(t, null);
                    }
                    catch (Exception ex)
                    {
                        returnDelegate.Invoke(null, ex);
                    }
                }
            }, cancel.Token);
            return cancel;
        }

        /// <summary>
        ///   Gets a cursor that contains all of the documents in the collection and returns the cursor through the callback action.
        /// </summary>
        /// <param name="returnDelegate">The callback delegate</param>
        /// <remarks>
        ///   Cursors load documents from the database in batches instead of all at once.
        /// </remarks>
        /// <returns>A <see cref="CancellationTokenSource"/> for cancelling the task.</returns>
        public CancellationTokenSource FindAllAsync(Action<ICursor<T>, Exception> returnDelegate)
        {
            var cancel = new CancellationTokenSource();
            new TaskFactory().StartNew(() =>
            {
                if (!cancel.Token.IsCancellationRequested)
                {
                    try
                    {
                        var t = FindAll();
                        returnDelegate.Invoke(t, null);
                    }
                    catch (Exception ex)
                    {
                        returnDelegate.Invoke(null, ex);
                    }
                }
            }, cancel.Token);
            return cancel;
        }

        /// <summary>
        /// Uses the $where operator to query the collection.  The value of the where is Javascript that will
        /// produce a true for the documents that match the criteria.
        /// Returns the cursor through the callback delegate.
        /// </summary>
        /// <param name="javascriptWhere">Javascript</param>
        /// <param name="returnDelegate">The callback delegate.</param>
        /// <returns>A <see cref="CancellationTokenSource"/> for cancelling the task.</returns>
        public CancellationTokenSource FindAsync(string javascriptWhere, Action<ICursor<T>, Exception> returnDelegate)
        {
            var cancel = new CancellationTokenSource();
            new TaskFactory().StartNew(() =>
            {
                if (!cancel.Token.IsCancellationRequested)
                {
                    try
                    {
                        var t = Find(javascriptWhere);
                        returnDelegate.Invoke(t, null);
                    }
                    catch (Exception ex)
                    {
                        returnDelegate.Invoke(null, ex);
                    }
                }
            }, cancel.Token);
            return cancel;
        }

        /// <summary>
        /// Queries the collection using the query selector and returns the cursor through the callback delegate.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="returnDelegate">The callback delegate.</param>
        /// <returns>A <see cref="CancellationTokenSource"/> for cancelling the task.</returns>
        public CancellationTokenSource FindAsync(object selector, Action<ICursor<T>, Exception> returnDelegate)
        {
            var cancel = new CancellationTokenSource();
            new TaskFactory().StartNew(() =>
            {
                if (!cancel.Token.IsCancellationRequested)
                {
                    try
                    {
                        var t = Find(selector);
                        returnDelegate.Invoke(t, null);
                    }
                    catch (Exception ex)
                    {
                        returnDelegate.Invoke(null, ex);
                    }
                }
            }, cancel.Token);
            return cancel;
        }

        /// <summary>
        /// Queries the collection using the specification and only returns a subset of fields through the callback delegate.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="fields">The fields.</param>
        /// <param name="returnDelegate">The callback delegate</param>
        /// <returns>A <see cref="CancellationTokenSource"/> for cancelling the task.</returns>
        public CancellationTokenSource FindAsync(object selector, object fields, Action<ICursor<T>, Exception> returnDelegate)
        {
            var cancel = new CancellationTokenSource();
            new TaskFactory().StartNew(() =>
            {
                if (!cancel.Token.IsCancellationRequested)
                {
                    try
                    {
                        var t = Find(selector, fields);
                        returnDelegate.Invoke(t, null);
                    }
                    catch (Exception ex)
                    {
                        returnDelegate.Invoke(null, ex);
                    }
                }
            }, cancel.Token);
            return cancel;
        }

        /// <summary>
        /// Executes a query and atomically applies a modifier operation to the first document returning the original document
        /// by default through the callback delegate.
        /// </summary>
        /// <param name="document">The document.</param>
        /// <param name="selector">The selector.</param>
        /// <param name="returnDelegate">The callback delegate.</param>
        /// <returns>A <see cref="CancellationTokenSource"/> for cancelling the task.</returns>
        public CancellationTokenSource FindAndModifyAsync(object document, object selector, Action<T, Exception> returnDelegate)
        {
            var cancel = new CancellationTokenSource();
            new TaskFactory().StartNew(() =>
            {
                if (!cancel.Token.IsCancellationRequested)
                {
                    try
                    {
                        var t = FindAndModify(document, selector);
                        returnDelegate.Invoke(t, null);
                    }
                    catch (Exception ex)
                    {
                        returnDelegate.Invoke(null, ex);
                    }
                }
            }, cancel.Token);
            return cancel;
        }

        /// <summary>
        /// Executes a query and atomically applies a modifier operation to the first document returning the original document
        /// by default through the callback delegate.
        /// </summary>
        /// <param name="document">The document.</param>
        /// <param name="selector">The selector.</param>
        /// <param name="sort"><see cref="Document"/> containing the names of columns to sort on with the values being the</param>
        /// <param name="returnDelegate">The callback delegate.</param>
        /// <returns>A <see cref="CancellationTokenSource"/> for cancelling the task.</returns>
        /// <see cref="IndexOrder"/>
        public CancellationTokenSource FindAndModifyAsync(object document, object selector, object sort, Action<T, Exception> returnDelegate)
        {
            var cancel = new CancellationTokenSource();
            new TaskFactory().StartNew(() =>
            {
                if (!cancel.Token.IsCancellationRequested)
                {
                    try
                    {
                        var t = FindAndModify(document, selector, sort);
                        returnDelegate.Invoke(t, null);
                    }
                    catch (Exception ex)
                    {
                        returnDelegate.Invoke(null, ex);
                    }
                }
            }, cancel.Token);
            return cancel;
        }

        /// <summary>
        /// Executes a query and atomically applies a modifier operation to the first document returning the original document
        /// by default through the callback delegate.
        /// </summary>
        /// <param name="document">The document.</param>
        /// <param name="selector">The selector.</param>
        /// <param name="returnNew">if set to <c>true</c> [return new].</param>
        /// <param name="returnDelegate">The callback delegate.</param>
        /// <returns>A <see cref="CancellationTokenSource"/> for cancelling the task.</returns>
        public CancellationTokenSource FindAndModifyAsync(object document, object selector, bool returnNew, Action<T, Exception> returnDelegate)
        {
            var cancel = new CancellationTokenSource();
            new TaskFactory().StartNew(() =>
            {
                if (!cancel.Token.IsCancellationRequested)
                {
                    try
                    {
                        var t = FindAndModify(document, selector, returnNew);
                        returnDelegate.Invoke(t, null);
                    }
                    catch (Exception ex)
                    {
                        returnDelegate.Invoke(null, ex);
                    }
                }
            }, cancel.Token);
            return cancel;
        }

        /// <summary>
        /// Executes a query and atomically applies a modifier operation to the first document returning the original document
        /// by default through the callback delegate.
        /// </summary>
        /// <param name="document">The document.</param>
        /// <param name="selector">The selector.</param>
        /// <param name="sort"><see cref="Document"/> containing the names of columns to sort on with the values being the
        /// <see cref="IndexOrder"/></param>
        /// <param name="returnNew">if set to <c>true</c> [return new].</param>
        /// <param name="returnDelegate">The callback delegate.</param>
        /// <returns>A <see cref="CancellationTokenSource"/> for cancelling the task.</returns>
        public CancellationTokenSource FindAndModifyAsync(object document, object selector, object sort, bool returnNew, Action<T, Exception> returnDelegate)
        {
            var cancel = new CancellationTokenSource();
            new TaskFactory().StartNew(() =>
            {
                if (!cancel.Token.IsCancellationRequested)
                {
                    try
                    {
                        var t = FindAndModify(document, selector, sort, returnNew);
                        returnDelegate.Invoke(t, null);
                    }
                    catch (Exception ex)
                    {
                        returnDelegate.Invoke(null, ex);
                    }
                }
            }, cancel.Token);
            return cancel;
        }

        /// <summary>
        /// Entrypoint into executing a map/reduce query against the collection.
        /// </summary>
        /// <param name="returnDelegate">The callback delegate.</param>
        /// <returns>A <see cref="CancellationTokenSource"/> for cancelling the task.</returns>
        public CancellationTokenSource MapReduceAsync(Action<MapReduce, Exception> returnDelegate)
        {
            var cancel = new CancellationTokenSource();
            new TaskFactory().StartNew(() =>
            {
                if (!cancel.Token.IsCancellationRequested)
                {
                    try
                    {
                        var t = MapReduce();
                        returnDelegate.Invoke(t, null);
                    }
                    catch (Exception ex)
                    {
                        returnDelegate.Invoke(null, ex);
                    }
                }
            }, cancel.Token);
            return cancel;
        }

        ///<summary>
        ///  Count all items in the collection.
        ///</summary>
        ///<param name="returnDelegate">The callback delegate</param>
        /// <returns>A <see cref="CancellationTokenSource"/> for cancelling the task.</returns>
        public CancellationTokenSource CountAsync(Action<long, Exception> returnDelegate)
        {
            var cancel = new CancellationTokenSource();
            new TaskFactory().StartNew(() =>
            {
                if (!cancel.Token.IsCancellationRequested)
                {
                    try
                    {
                        var t = Count();
                        returnDelegate.Invoke(t, null);
                    }
                    catch (Exception ex)
                    {
                        returnDelegate.Invoke(0, ex);
                    }
                }
            }, cancel.Token);
            return cancel;

        }

        /// <summary>
        ///   Count all items in a collection that match the query selector.
        /// </summary>
        /// <param name = "selector">The selector.</param>
        /// <param name="returnDelegate">The callback delegate.</param>
        /// <returns>A <see cref="CancellationTokenSource"/> for cancelling the task.</returns>
        /// <remarks>
        ///   It will return 0 if the collection doesn't exist yet.
        /// </remarks>
        public CancellationTokenSource CountAsync(object selector, Action<long, Exception> returnDelegate)
        {
            var cancel = new CancellationTokenSource();
            new TaskFactory().StartNew(() =>
            {
                if (!cancel.Token.IsCancellationRequested)
                {
                    try
                    {
                        var t = Count(selector);
                        returnDelegate.Invoke(t, null);
                    }
                    catch (Exception ex)
                    {
                        returnDelegate.Invoke(0, ex);
                    }
                }
            }, cancel.Token);
            return cancel;
        }

        /// <summary>
        ///   Inserts the Document into the collection.
        /// </summary>
        /// <param name="document">The doc.</param>
        /// <param name="exceptionHandler">The callback delegate for handling exceptions.</param>
        /// <returns>A <see cref="CancellationTokenSource"/> for cancelling the task.</returns>
        public CancellationTokenSource InsertAsync(object document, bool safemode, Action<Exception> exceptionHandler)
        {
            var cancel = new CancellationTokenSource();
            new TaskFactory().StartNew(() =>
            {
                if (!cancel.Token.IsCancellationRequested)
                {
                    try
                    {
                        Insert(document, safemode);
                        exceptionHandler.Invoke(null);
                    }
                    catch (Exception ex)
                    {
                        exceptionHandler.Invoke(ex);
                    }
                }
            }, cancel.Token);
            return cancel;
        }

        /// <summary>
        ///   Inserts the specified doc.
        /// </summary>
        /// <param name = "document">The doc.</param>
        /// <param name="exceptionHandler">The callback delegate for handling exceptions.</param>
        /// <returns>A <see cref="CancellationTokenSource"/> for cancelling the task.</returns>
        public CancellationTokenSource InsertAsync(object document, Action<Exception> exceptionHandler)
        {
            var cancel = new CancellationTokenSource();
            new TaskFactory().StartNew(() =>
            {
                if (!cancel.Token.IsCancellationRequested)
                {
                    try
                    {
                        Insert(document);
                        exceptionHandler.Invoke(null);
                    }
                    catch (Exception ex)
                    {
                         exceptionHandler.Invoke(ex);
                    }
                }
            }, cancel.Token);
            return cancel;
        }

        /// <summary>
        ///   Bulk inserts the specified documents into the database.
        /// </summary>
        /// <param name="documents">The docs.</param>
        /// <param name="exceptionHandler">The callback delegate for handling exceptions.</param>
        /// <remarks>
        ///   See the safemode description in the class description
        /// </remarks>
        /// <returns>A <see cref="CancellationTokenSource"/> for cancelling the task.</returns>
        public CancellationTokenSource InsertAsync<TElement>(IEnumerable<TElement> documents, bool safemode, Action<Exception> exceptionHandler)
        {
            var cancel = new CancellationTokenSource();
            new TaskFactory().StartNew(() =>
            {
                if (!cancel.Token.IsCancellationRequested)
                {
                    try
                    {
                        Insert<TElement>(documents, safemode);
                        exceptionHandler.Invoke(null);
                    }
                    catch (Exception ex)
                    {
                        exceptionHandler.Invoke(ex);
                    }
                }
            }, cancel.Token);
            return cancel;
        }

        /// <summary>
        ///   Bulk inserts the specified documents into the database.
        /// </summary>
        /// <param name = "documents">The documents.</param>
        /// <param name="exceptionHandler">The callback delegate for handling exceptions.</param>
        /// <returns>A <see cref="CancellationTokenSource"/> for cancelling the task.</returns>
        public CancellationTokenSource InsertAsync<TElement>(IEnumerable<TElement> documents, Action<Exception> exceptionHandler)
        {
            var cancel = new CancellationTokenSource();
            new TaskFactory().StartNew(() =>
            {
                if (!cancel.Token.IsCancellationRequested)
                {
                    try
                    {
                        Insert<TElement>(documents);
                        exceptionHandler.Invoke(null);
                    }
                    catch (Exception ex)
                    {
                        exceptionHandler.Invoke(ex);
                    }
                }
            }, cancel.Token);
            return cancel;
        }

        /// <summary>
        ///   Remove documents from the collection according to the selector.
        /// </summary>
        /// <param name = "selector">The selector.</param>
        /// <param name = "safemode">if set to <c>true</c> [safemode].</param>
        /// <param name="exceptionHandler">The callback delegate for handling exceptions.</param>
        /// <remarks>
        ///   An empty document will match all documents in the collection and effectively truncate it.
        ///   See the safemode description in the class description
        /// </remarks>
        /// <returns>A <see cref="CancellationTokenSource"/> for cancelling the task.</returns>
        public CancellationTokenSource RemoveAsync(object selector, bool safemode, Action<Exception> exceptionHandler)
        {
            var cancel = new CancellationTokenSource();
            new TaskFactory().StartNew(() =>
            {
                if (!cancel.Token.IsCancellationRequested)
                {
                    try
                    {
                        Remove(selector, safemode);
                        exceptionHandler.Invoke(null);
                    }
                    catch (Exception ex)
                    {
                        exceptionHandler.Invoke(ex);
                    }
                }
            }, cancel.Token);
            return cancel;
        }

        /// <summary>
        /// Remove documents from the collection according to the selector.
        /// </summary>
        /// <param name="exceptionHandler">The callback delegate for handling exceptions.</param>
        /// <param name="selector">The selector.</param>
        /// <remarks>
        /// An empty document will match all documents in the collection and effectively truncate it.
        /// </remarks>
        /// <returns>A <see cref="CancellationTokenSource"/> for cancelling the task.</returns>
        public CancellationTokenSource RemoveAsync(object selector, Action<Exception> exceptionHandler)
        {
            var cancel = new CancellationTokenSource();
            new TaskFactory().StartNew(() =>
            {
                if (!cancel.Token.IsCancellationRequested)
                {
                    try
                    {
                        Remove(selector);
                        exceptionHandler.Invoke(null);
                    }
                    catch (Exception ex)
                    {
                        exceptionHandler.Invoke(ex);
                    }
                }
            }, cancel.Token);
            return cancel;
        }

        /// <summary>
        ///   Updates the specified document with the current document.  In order to only do a partial update use a
        ///   document containing modifier operations ($set, $unset, $inc, etc.)
        /// </summary>
        /// <param name = "document">The document.</param>
        /// <param name = "selector">The selector.</param>
        /// <param name = "safemode">if set to <c>true</c> [safemode].</param>
        /// <param name="exceptionHandler">The callback delegate for handling exceptions.</param>
        /// <remarks>
        ///   See the safemode description in the class description
        /// </remarks>
        /// <returns>A <see cref="CancellationTokenSource"/> for cancelling the task.</returns>
        public CancellationTokenSource UpdateAsync(object document, object selector, bool safemode, Action<Exception> exceptionHandler)
        {
            var cancel = new CancellationTokenSource();
            new TaskFactory().StartNew(() =>
            {
                if (!cancel.Token.IsCancellationRequested)
                {
                    try
                    {
                        Update(document, selector, safemode);
                        exceptionHandler.Invoke(null);
                    }
                    catch (Exception ex)
                    {
                        exceptionHandler.Invoke(ex);
                    }
                }
            }, cancel.Token);
            return cancel;
        }

        /// <summary>
        ///   Updates the specified document with the current document.  In order to only do a partial update use a 
        ///   document containing modifier operations ($set, $unset, $inc, etc.)
        /// </summary>
        /// <param name = "document">The document.</param>
        /// <param name = "selector">The selector.</param>
        /// <param name="exceptionHandler">The callback delegate for handling exceptions.</param>
        /// <returns>A <see cref="CancellationTokenSource"/> for cancelling the task.</returns>     
        public CancellationTokenSource UpdateAsync(object document, object selector, Action<Exception> exceptionHandler)
        {
            var cancel = new CancellationTokenSource();
            new TaskFactory().StartNew(() =>
            {
                if (!cancel.Token.IsCancellationRequested)
                {
                    try
                    {
                        Update(document, selector);
                        exceptionHandler.Invoke(null);
                    }
                    catch (Exception ex)
                    {
                        exceptionHandler.Invoke(ex);
                    }
                }
            }, cancel.Token);
            return cancel;
        }

        /// <summary>
        ///   Updates the specified document with the current document.  In order to only do a partial update use a
        ///   document containing modifier operations ($set, $unset, $inc, etc.)
        /// </summary>
        /// <param name = "document">The document.</param>
        /// <param name = "selector">The selector.</param>
        /// <param name = "flags">The flags.</param>
        /// <param name = "safemode">if set to <c>true</c> [safemode].</param>
        /// <param name="exceptionHandler">The callback delegate to handle exceptions.</param>
        /// <remarks>
        ///   See the safemode description in the class description
        /// </remarks>
        /// <returns>A <see cref="CancellationTokenSource"/> for cancelling the task.</returns>        
        public CancellationTokenSource UpdateAsync(object document, object selector, UpdateFlags flags, bool safemode, Action<Exception> exceptionHandler)
        {
            var cancel = new CancellationTokenSource();
            new TaskFactory().StartNew(() =>
            {
                if (!cancel.Token.IsCancellationRequested)
                {
                    try
                    {
                        Update(document, selector, flags, safemode);
                        exceptionHandler.Invoke(null);
                    }
                    catch (Exception ex)
                    {
                        exceptionHandler.Invoke(ex);
                    }
                }
            }, cancel.Token);
            return cancel;
        }

        /// <summary>
        ///   Updates the specified document with the current document.  In order to only do a partial update use a 
        ///   document containing modifier operations ($set, $unset, $inc, etc.)
        /// </summary>
        /// <param name = "document">The <see cref = "Document" /> to update with</param>
        /// <param name = "selector">The query selector to find the document to update.</param>
        /// <param name = "flags"><see cref = "UpdateFlags" /></param>
        /// <param name="exceptionHandler">The callback delegate for handling exceptions.</param>
        /// <returns>A <see cref="CancellationTokenSource"/> for cancelling the task.</returns>
        public CancellationTokenSource UpdateAsync(object document, object selector, UpdateFlags flags, Action<Exception> exceptionHandler)
        {
            var cancel = new CancellationTokenSource();
            new TaskFactory().StartNew(() =>
            {
                if (!cancel.Token.IsCancellationRequested)
                {
                    try
                    {
                        Update(document, selector, flags);
                        exceptionHandler.Invoke(null);
                    }
                    catch (Exception ex)
                    {
                        exceptionHandler.Invoke(ex);
                    }
                }
            }, cancel.Token);
            return cancel;
        }

        /// <summary>
        ///   Runs a multiple update query against the database.  It will wrap any
        ///   doc with $set if the passed in doc doesn't contain any '$' modifier ops.
        /// </summary>
        /// <param name = "document">The document.</param>
        /// <param name = "selector">The selector.</param>
        /// <param name="exceptionHandler">The callback delegate for handling exceptions.</param>
        /// <returns>A <see cref="CancellationTokenSource"/> for cancelling the task.</returns>
        public CancellationTokenSource UpdateAllAsync(object document, object selector, Action<Exception> exceptionHandler)
        {
            var cancel = new CancellationTokenSource();
            new TaskFactory().StartNew(() =>
            {
                if (!cancel.Token.IsCancellationRequested)
                {
                    try
                    {
                        UpdateAll(document, selector);
                        exceptionHandler.Invoke(null);
                    }
                    catch (Exception ex)
                    {
                        exceptionHandler.Invoke(ex);
                    }
                }
            }, cancel.Token);
            return cancel;
        }

        /// <summary>
        ///   Runs a multiple update query against the database.  It will wrap any
        ///   doc with $set if the passed in doc doesn't contain any '$' modifier ops.
        /// </summary>
        /// <param name = "document">The document.</param>
        /// <param name = "selector">The selector.</param>
        /// <param name = "safemode">if set to <c>true</c> [safemode].</param>
        /// <param name="exceptionHandler">The callback delegate for handling exceptions.</param>
        /// <remarks>
        ///   See the safemode description in the class description
        /// </remarks>
        /// <returns>A <see cref="CancellationTokenSource"/> for cancelling the task.</returns>
        public CancellationTokenSource UpdateAllAsync(object document, object selector, bool safemode, Action<Exception> exceptionHandler)
        {
            var cancel = new CancellationTokenSource();
            new TaskFactory().StartNew(() =>
            {
                if (!cancel.Token.IsCancellationRequested)
                {
                    try
                    {
                        UpdateAll(document, selector, safemode);
                        exceptionHandler.Invoke(null);
                    }
                    catch (Exception ex)
                    {
                        exceptionHandler.Invoke(ex);
                    }
                }
            }, cancel.Token);
            return cancel;
        }

        /// <summary>
        ///   Inserts or updates a document in the database.  If the document does not contain an _id one will be
        ///   generated and an upsert sent.  Otherwise the document matching the _id of the document will be updated.
        /// </summary>
        /// <param name = "document">The document.</param>
        /// <param name="exceptionHandler">The callback delegate for handling exceptions.</param>
        /// <remarks>
        ///   The document will contain the _id that is saved to the database.
        /// </remarks>
        /// <returns>A <see cref="CancellationTokenSource"/> for cancelling the task.</returns>
        public CancellationTokenSource SaveAsync(object document, Action<Exception> exceptionHandler)
        {
            var cancel = new CancellationTokenSource();
            new TaskFactory().StartNew(() =>
            {
                if (!cancel.Token.IsCancellationRequested)
                {
                    try
                    {
                        Save(document);
                        exceptionHandler.Invoke(null);
                    }
                    catch (Exception ex)
                    {
                        exceptionHandler.Invoke(ex);
                    }
                }
            }, cancel.Token);
            return cancel;
        }

        /// <summary>
        ///   Saves a document to the database using an upsert.
        /// </summary>
        /// <param name = "document">The document.</param>
        /// <param name = "safemode">if set to <c>true</c> [safemode].</param>
        /// <param name="exceptionHandler">The callback delegate for handling exceptions.</param>
        /// <returns>A <see cref="CancellationTokenSource"/> for cancelling the task.</returns>
        public CancellationTokenSource SaveAsync(object document, bool safemode, Action<Exception> exceptionHandler)
        {
            var cancel = new CancellationTokenSource();
            new TaskFactory().StartNew(() =>
            {
                if (!cancel.Token.IsCancellationRequested)
                {
                    try
                    {
                        Save(document, safemode);
                        exceptionHandler.Invoke(null);
                    }
                    catch (Exception ex)
                    {
                        exceptionHandler.Invoke(ex);
                    }
                }
            }, cancel.Token);
            return cancel;
        }
    }
}

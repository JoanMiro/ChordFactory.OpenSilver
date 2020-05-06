namespace ChordFactory.OpenSilver.models
{
    using System.Collections.Generic;

    public interface INoteSequence
    {
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        string Description { get; set; }

        /// <summary>
        /// Gets or sets the notes.
        /// </summary>
        /// <value>The notes.</value>
        List<int> Notes { get; set; }
    }
}
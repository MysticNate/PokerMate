class Signature
{
    public ISigner signer { get; set; }  // Could be a any signer
    public DateTimeOffset? signedAt { get; set; } // if null means not signed
    public bool isSigned => signedAt.HasValue;  // True if date exists

    public Signature(ISigner signer)
    {
        this.signer = signer;
        signedAt = DateTimeOffset.Now.DateTime; // Automatically set timestamp
    }

}

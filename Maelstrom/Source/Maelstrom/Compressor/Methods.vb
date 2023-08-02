Friend Module Compressor
    Public Function Compress(ByRef Input as Byte(), ByRef Output as Byte(), ByVal Count As Int32) As Int32
        Dim CompressedMemStream = New IO.MemoryStream()
        Dim Compressor = New IO.Compression.GZipStream(CompressedMemStream, IO.Compression.CompressionMode.Compress)
        
        Compressor.Write(Input, 0, Count)
        Compressor.Close()
        Dim TempBuffer = CompressedMemStream.ToArray()
        
        If Output.Length < TempBuffer.Length Then Redim Output(TempBuffer.Length - 1)
        Buffer.BlockCopy(TempBuffer,0,Output,0,TempBuffer.Length)
        
        Compressor.Dispose()
        CompressedMemStream.Dispose()
        Return TempBuffer.Length
    End Function
    PUblic Function Decompress(ByRef Input As Byte(), ByRef Output as Byte(), ByVal Count As Int32) As Int32
        Dim InternalBuffer As Byte() = Nothing
        Array.Resize(InternalBuffer, Count)
        Buffer.BlockCopy(Input, 0, InternalBuffer, 0, Count)
        Dim CompressedMemStream = New IO.MemoryStream(InternalBuffer)
        Dim Decompressor = New IO.Compression.GZipStream(CompressedMemStream, IO.Compression.CompressionMode.Decompress)
        Dim DecompressedMemStream = New IO.MemoryStream()
        
        Decompressor.CopyTo(DecompressedMemStream)
        Dim TempBuffer = DecompressedMemStream.ToArray()
        If Output.Length < TempBuffer.Length then redim Output(TempBuffer.Length - 1)
        Buffer.BlockCopy(TempBuffer,0,Output,0,TempBuffer.Length)
        
        CompressedMemStream.Dispose()
        Decompressor.Dispose()
        DecompressedMemStream.Dispose()
        Return TempBuffer.Length
    End Function
End Module
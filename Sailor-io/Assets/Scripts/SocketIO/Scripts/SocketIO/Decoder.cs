#region License
/*
 * Decoder.cs
 *
 * The MIT License
 *
 * Copyright (c) 2014 Fabio Panettieri
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */
#endregion

//#define SOCKET_IO_DEBUG			// Uncomment this for debug
using System;
using System.Collections;
using System.Text;
using UnityEngine;
using WebSocketSharp;

namespace SocketIO
{
	public class Decoder
	{
		public Packet Decode(MessageEventArgs e)
		{
			try
			{
				#if SOCKET_IO_DEBUG
				Debug.Log("[SocketIO] Decoding: " + e.Data);
				#endif
				Packet packet = new Packet();

				// look up packet type
				if (e.Data == "Binary")
				{
					packet.rawData = e.RawData;
					packet.socketPacketType = SocketPacketType.BINARY_EVENT;
					return packet;
				}
				else
				{
					return null;
				}

			} catch(Exception ex) {
				throw new SocketIOException("Packet decoding failed: " + e.Data ,ex);
			}
		}
	}
}

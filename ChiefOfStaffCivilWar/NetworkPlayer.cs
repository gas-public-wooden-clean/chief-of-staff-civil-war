using System;
using System.Globalization;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChiefOfStaffCivilWar
{
	class NetworkPlayer : TeamController
	{
		public NetworkPlayer(TcpListener listener)
		{
			_listener = listener;
			_client = listener.AcceptSocketAsync();
			_buffer = new byte[16];
			_bufferStart = 0;
		}

		static readonly Encoding _strictUtf8 = new UTF8Encoding(false, true);
		readonly TcpListener _listener;
		Task<Socket> _client;
		byte[] _buffer;
		int _bufferStart;
		int _bufferEnd;

		public async Task SendMessage(string message, CancellationToken cancellationToken)
		{
			byte[] bytes = _strictUtf8.GetBytes(message.Replace("\n", "\r\n"));
			Socket socket = await _client;
			await socket.SendAsync(bytes.AsMemory(), SocketFlags.None, cancellationToken);
		}

		public Task SendMessage(string format, CancellationToken cancellationToken, params object[] args)
		{
			return SendMessage(string.Format(CultureInfo.CurrentCulture, format, args), cancellationToken);
		}

		public async Task<int> GetMenuOption(int max, CancellationToken cancellationToken)
		{
			string input = await ReadLine(cancellationToken);
			int selection;
			while (!int.TryParse(input, NumberStyles.None, CultureInfo.CurrentCulture, out selection) ||
				selection < 1 || selection > max)
			{
				await SendMessage("Invalid option. Try again: ", cancellationToken);
				input = await ReadLine(cancellationToken);
			}

			return selection;
		}

		async Task<string> ReadLine(CancellationToken cancellationToken)
		{
			Socket socket = await _client;

			while (_bufferStart < _bufferEnd && IsNewline(_buffer[_bufferStart]))
			{
				_bufferStart += 1;
			}

			int newlineIndex;
			while ((newlineIndex = GetNewlineIndex()) < 0)
			{
				if (_bufferEnd >= _buffer.Length)
				{
					byte[] newBuffer = new byte[2 * _buffer.Length];
					Array.Copy(_buffer, newBuffer, _buffer.Length);
					_buffer = newBuffer;
				}

				int numRead;
				while ((numRead = await ReceiveAsync(socket, _buffer.AsMemory(_bufferEnd), cancellationToken)) <= 0)
				{
					_client = _listener.AcceptSocketAsync();
					socket = await _client;
				}

				_bufferEnd += numRead;
			}

			string retval = _strictUtf8.GetString(_buffer.AsSpan(_bufferStart, newlineIndex - _bufferStart));
			_bufferEnd -= newlineIndex + 1;
			Array.Copy(_buffer, newlineIndex + 1, _buffer, 0, _bufferEnd);
			_bufferStart = 0;
			return retval;
		}

		static async Task<int> ReceiveAsync(Socket socket, Memory<byte> buffer, CancellationToken cancellationToken)
		{
			ValueTask<int> result = socket.ReceiveAsync(buffer, SocketFlags.None, cancellationToken);
			try
			{
				return await result;
			}
			catch (SocketException)
			{
				return 0;
			}
		}

		int GetNewlineIndex()
		{
			for (int newlineIndex = _bufferStart; newlineIndex < _bufferEnd; newlineIndex++)
			{
				if (IsNewline(_buffer[newlineIndex]))
				{
					return newlineIndex;
				}
			}

			return -1;
		}

		static bool IsNewline(byte utf8Byte)
		{
			return utf8Byte is 10 or 13;
		}
	}
}

﻿using MimeKit;
using SmtpServer;
using SmtpServer.Protocol;
using SmtpServer.Storage;
using System.Buffers;

namespace SmtpDemoTests.Fixtures.Email.Hooks
{
    public class InMemoryMessageStore : MessageStore
    {
        public static MimeMessage SentMessage { get; private set; }

        public InMemoryMessageStore()
        {
            SentMessage = null;
        }
        public override async Task<SmtpResponse> SaveAsync(ISessionContext context, IMessageTransaction transaction, ReadOnlySequence<byte> buffer, CancellationToken cancellationToken)
        {
            MimeMessage message = await GetEmailContent(buffer, cancellationToken);
            StoreMessage(message);
            return SmtpResponse.Ok;
        }

        private async Task<MimeMessage> GetEmailContent(ReadOnlySequence<byte> buffer, CancellationToken cancellationToken)
        {
            await using var stream = new MemoryStream();

            var position = buffer.GetPosition(0);
            while (buffer.TryGet(ref position, out var memory))
            {
                await stream.WriteAsync(memory, cancellationToken);
            }
            stream.Position = 0;

            return await MimeMessage.LoadAsync(stream, cancellationToken);
        }

        private void StoreMessage(MimeMessage message)
        {
            SentMessage = message;
        }
    }
}

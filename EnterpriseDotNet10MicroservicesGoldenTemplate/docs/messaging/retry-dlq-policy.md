# Retry and DLQ Policy

## Default policy
Retries are bounded. The default reference policy is:

| Attempt | Delay |
|---|---:|
| 1 | immediate |
| 2 | 5 seconds |
| 3 | 30 seconds |
| 4 | 2 minutes |
| Final | DLQ |

These values are a reference baseline and may be changed by platform/SRE governance.

## Retry classification
- Transient infrastructure failure: retry.
- Temporary dependency unavailable: retry.
- Validation/schema failure: do not retry indefinitely; DLQ.
- Authentication/authorization failure: DLQ and alert.
- Business rule rejection: normally DLQ or explicit business-rejection event.
- Poison message: DLQ.

## Idempotency
A consumer must register the event ID before executing side effects, using an atomic uniqueness constraint. If the ID already exists, acknowledge the message without repeating the side effect.

## Replay
DLQ replay must be an explicit operational action. Replay must preserve the original event ID so Inbox idempotency remains effective unless a controlled reprocessing strategy explicitly changes the identity.

# Moonglade.Custodian

Azure Function for Moonglade that do maintenance work

## Configuration

### Environment Variables

| Name | Description |
| ---- | ----------- |
| `STORAGE_CONNSTR` | Connection string to Moonglade storage account |
| `SOURCE_CONTAINER` | Blog image container name (watermarked images) |
| `DEST_CONTAINER` | Blog origin image container name |
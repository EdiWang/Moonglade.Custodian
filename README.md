# Moonglade.Custodian

Azure Function for [Moonglade](https://github.com/EdiWang/Moonglade) that do maintenance work

> Starting from Moonglade v14.19.0, this project is no longer needed. The function is integrated into Moonglade project. This repo is archived.

## Configuration

### Environment Variables

| Name | Description |
| ---- | ----------- |
| `STORAGE_CONNSTR` | Connection string to Moonglade storage account |
| `SOURCE_CONTAINER` | Blog image container name (watermarked images) |
| `DEST_CONTAINER` | Blog origin image container name |

## Function

### Origin Image Mover

Move origin image (without watermark) from public image container to desired container, usually private

![Snipaste_2023-12-28_10-49-54](https://github.com/EdiWang/Moonglade.Custodian/assets/3304703/4045e200-9059-4323-9e61-ba141d94fb3d)


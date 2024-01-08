# Moonglade.Custodian

Azure Function for [Moonglade](https://github.com/EdiWang/Moonglade) that do maintenance work

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

## 免责申明

对于中国用户，我们有一份特定的免责申明。请确保你已经阅读并理解其内容：

- [免责申明（仅限中国用户）](./DISCLAIMER_CN.md)

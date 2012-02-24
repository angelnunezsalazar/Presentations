package domain;

public class ImageInfo {

	public String path;

	public ImageInfo(String path) {
		this.path = path;
	}

	public String getPath() {
		return path;
	}

	/**
	 * @return the type of the image
	 */
	public String getImageType() {
		return path.substring(path.indexOf(".") + 1);

	}
}

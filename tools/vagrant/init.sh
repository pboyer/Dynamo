echo "======Welcome to Dynamo Linux======"

# Add LibG to ld.so.cache, this allows LibG and ASM to be discovered at runtime
if grep -q "LibG" "/etc/ld.so.conf"; then
	echo "LibG already in /etc/ld.so.conf"
else
	sudo sh -c 'echo /LibG >> /etc/ld.so.conf'
	sudo ldconfig
	echo "LibG added to /etc/ld.so.conf"
fi

exit 0
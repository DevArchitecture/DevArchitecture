# flutter_devarchitecture

cd ios
rm -rf Podfile.lock Pods
pod deintegrate
pod setup
pod install
cd ..
flutter build ios


cd macos
rm -rf Podfile.lock Pods
pod deintegrate
pod setup
pod install
cd ..
flutter build macos

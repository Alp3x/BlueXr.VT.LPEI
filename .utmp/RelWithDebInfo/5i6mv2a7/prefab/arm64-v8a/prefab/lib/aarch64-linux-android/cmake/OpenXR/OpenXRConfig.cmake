if(NOT TARGET OpenXR::openxr_loader)
add_library(OpenXR::openxr_loader SHARED IMPORTED)
set_target_properties(OpenXR::openxr_loader PROPERTIES
    IMPORTED_LOCATION "C:/Users/Alp3x/.gradle/caches/8.11/transforms/89e4f5936f3bcc0906330953f2428a44/transformed/jetified-openxr_loader/prefab/modules/openxr_loader/libs/android.arm64-v8a/libopenxr_loader.so"
    INTERFACE_INCLUDE_DIRECTORIES "C:/Users/Alp3x/.gradle/caches/8.11/transforms/89e4f5936f3bcc0906330953f2428a44/transformed/jetified-openxr_loader/prefab/modules/openxr_loader/include"
    INTERFACE_LINK_LIBRARIES ""
)
endif()

